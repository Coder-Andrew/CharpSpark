using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResuMeta.Data;
using ResuMeta.Models;
using ResuMeta.Utilities;
using ResuMeta.DAL.Concrete;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using ResuMeta.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AuthConnection") ?? throw new InvalidOperationException("Connection string 'AuthConnection' not found.");
var resuMetaConnectionString = builder.Configuration.GetConnectionString("ResuMetaConnection") ?? throw new InvalidOperationException("Connection string 'ResuMetaConnection' not found.");
var chatGPTApiKey = builder.Configuration["ChatGPTAPIKey"] ?? throw new InvalidOperationException("Connection string 'ChatGPTAPIKey' not found.");

string chatGPTUrl = "https://api.openai.com/";

builder.Services.AddHttpClient<IChatGPTService, ChatGPTService>((httpClient, services) =>
{
    httpClient.BaseAddress = new Uri(chatGPTUrl);
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", chatGPTApiKey);
    return new ChatGPTService(httpClient, services.GetRequiredService<ILogger<ChatGPTService>>());
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<ResuMetaDbContext>(options => options
    .UseLazyLoadingProxies()
    .UseSqlServer(resuMetaConnectionString)
);

builder.Services.AddScoped<DbContext, ResuMetaDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<ISkillsRepository, SkillsRepository>();
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<IApplicationTrackerRepository, ApplicationTrackerRepository>();
builder.Services.AddScoped<IApplicationTrackerService, ApplicationTrackerService>();
builder.Services.AddSwaggerGen();

// Used for testing the service without using the Chatgpt API, uncomment to use
//builder.Services.AddScoped<IChatGPTService, FakeChatGPTService>();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();       
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var config = services.GetRequiredService<IConfiguration>();
        var seedUserPw = config["SeedUserPw"];
        await SeedUsers.Initialize(services, SeedData.UserSeedData, seedUserPw!);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
