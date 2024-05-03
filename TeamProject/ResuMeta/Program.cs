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
using Microsoft.Extensions.Options;
using Hangfire;
using SendGrid;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("AuthConnection") ?? throw new InvalidOperationException("Connection string 'AuthConnection' not found.");
var resuMetaConnectionString = builder.Configuration.GetConnectionString("ResuMetaConnection") ?? throw new InvalidOperationException("Connection string 'ResuMetaConnection' not found.");
var chatGPTApiKey = builder.Configuration["ChatGPTAPIKey"] ?? throw new InvalidOperationException("Connection string 'ChatGPTAPIKey' not found.");
var sendGridApiKey = builder.Configuration["SendGridApiKey"] ?? throw new InvalidOperationException("Connection string 'SendGridApiKey' not found.");
var sendFromEmail = builder.Configuration["SendFromEmail"] ?? throw new InvalidOperationException("Connection string 'SendFromEmail' not found.");
string chatGPTUrl = "https://api.openai.com/";


//builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});


builder.Services.AddHttpClient<IChatGPTService, ChatGPTService>((httpClient, services) =>
{
    httpClient.BaseAddress = new Uri(chatGPTUrl);
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", chatGPTApiKey);
    //return new ChatGPTService(httpClient, services.GetRequiredService<ILogger<ChatGPTService>>());
    return new ChatGPTService(
    httpClient, 
    services.GetRequiredService<ILogger<ChatGPTService>>(),
    services.GetRequiredService<IResumeRepository>(),
    services.GetRequiredService<ICoverLetterRepository>()
);
});

string nodeUrl = builder.Configuration["NodeUrl"] ?? throw new InvalidOperationException("Connection string 'NodeUrl' not found.");
builder.Services.Configure<NodeServiceOptions>(options =>
{
    options.NodeUrl = nodeUrl;
});

string scraperUrl = builder.Configuration["ScraperUrl"] ?? throw new InvalidOperationException("Connection string 'ScraperUrl' not found.");
builder.Services.Configure<WebScraperServiceOptions>(options =>
{
    options.ScraperUrl = scraperUrl;
});

builder.Services.AddHttpClient<INodeService, NodeService>((httpClient, services) =>
{
    httpClient.BaseAddress = new Uri(nodeUrl);
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    return new NodeService(
        httpClient, 
        services.GetRequiredService<IOptions<NodeServiceOptions>>(), 
        services.GetRequiredService<IRepository<Resume>>(),
        services.GetRequiredService<IRepository<UserInfo>>()
        );
});

builder.Services.AddHangfire(configuration => configuration
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseInMemoryStorage());

builder.Services.AddHangfireServer();

builder.Services.AddHttpClient<IWebScraperService, WebScraperService>((httpClient, services) =>
{
    httpClient.BaseAddress = new Uri(scraperUrl);
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    return new WebScraperService(
        httpClient,
        services.GetRequiredService<IOptions<WebScraperServiceOptions>>()
        );
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<ResuMetaDbContext>(options => options
    .UseLazyLoadingProxies()
    .UseSqlServer(resuMetaConnectionString)
);

builder.Services.AddScoped<DbContext, ResuMetaDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<ICoverLetterService, CoverLetterService>();
builder.Services.AddScoped<IResumeTemplateService, ResumeTemplateService>();
builder.Services.AddScoped<ISkillsRepository, SkillsRepository>();
builder.Services.AddScoped<IResumeRepository, ResumeRepository>();
builder.Services.AddScoped<ICoverLetterRepository, CoverLetterRepository>();
builder.Services.AddScoped<IResumeTemplateRepository, ResumeTemplateRepository>();
builder.Services.AddScoped<IApplicationTrackerRepository, ApplicationTrackerRepository>();
builder.Services.AddScoped<IApplicationTrackerService, ApplicationTrackerService>();
builder.Services.AddScoped<SendGridClient>(provider => new SendGridClient(sendGridApiKey));
builder.Services.AddScoped<ISendGridService, SendGridService>();


//builder.Services.AddScoped<INodeService, NodeService>();
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
    app.MapHangfireDashboard("/hangfire");
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
