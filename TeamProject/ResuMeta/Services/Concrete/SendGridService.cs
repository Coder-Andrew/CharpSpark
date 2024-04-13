using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using ResuMeta.Models;
using System.Text.Json;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using ResuMeta.Data;
using Microsoft.AspNetCore.Components.Forms;

namespace ResuMeta.Services.Concrete
{
    public class SendGridService : ISendGridService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SendGridService> _logger;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IRepository<ApplicationTracker> _applicationTracker;
        private readonly IApplicationTrackerService _applicationTrackerService;
        private readonly SendGridClient _client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SendGridService(
        ILogger<SendGridService> logger,
        UserManager<ApplicationUser> userManager,
        IRepository<UserInfo> userInfo,
        IRepository<ApplicationTracker> applicationTracker,
        IApplicationTrackerService applicationTrackerService,
        IConfiguration configuration,
        SendGridClient client,
        IHttpContextAccessor httpContextAccessor
        )
        {
            _client = client;
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _applicationTracker = applicationTracker;
            _applicationTrackerService = applicationTrackerService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


public async Task TestReminder2(string userId)
{
    var applications = _applicationTrackerService.GetApplicationsByUserId(int.Parse(userId));
      string id = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        _logger.LogInformation($"User with email {currUser.Email}  found. ");
        string email = currUser.Email;
    foreach (var application in applications)
    {
        // string id = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
        // var user = await _userManager.FindByIdAsync(id);
        // string email = user?.Email;
        // UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
        // _logger.LogInformation($"User with name {currUser.FirstName} and email {email} found.");
  
        
        // var user = await _userManager.FindByIdAsync(userId2);
        // //ApplicationUser user = await _userManager.FindByIdAsync(userId);
        // if (user == null)
        // {
        //     _logger.LogError($"User with ID {userId} not found. ");
        //     return;
        // }

        // string email = user.Email;
        // if (string.IsNullOrEmpty(email))
        // {
        //     _logger.LogError($"Email not found for user with ID {userId}.");
        //     return;
        // }
 
        // UserInfo userInfo = _userInfo.FindById(userId);
        // _logger.LogError($"User with name {userInfo.FirstName}  found. ");
        // if (userInfo == null)
        // {
        //     _logger.LogError($"User with ID {userId} not found. ");
        //     return;
        // }

    //     string email = user.Email;
    //     if (string.IsNullOrEmpty(email))
    //     {
    //         _logger.LogError($"Email not found for user with ID {userId}.");
    //         return;
    //     }


    var from = new EmailAddress("charpspark@gmail.com", "ResuMeta");
    var subject = $"Application for {application.CompanyName}";
            var to = new EmailAddress(email, "Danielle");
            var plainTextContent = $"Don't forget to apply for the {application.JobTitle} position at {application.CompanyName}!";
            var htmlContent = "<strong>Hello, World!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
           var response = await _client.SendEmailAsync(msg);
           _logger.LogInformation($"Email sent to {email}");
            _logger.LogInformation($"Application for {application.CompanyName}");

            }
        }
        


        // public async Task SendEmail(EmailAddress to, string subject, string plainTextContent, string htmlContent)
        // {
        //     var client = new SendGridClient(apiKey);
        //     var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //     var response = await client.SendEmailAsync(msg);
        // }

        // public async Task SendApplicationDeadlineReminder(string userId)
        // {
        //     var applications = _applicationTrackerService.GetApplicationsByUserId(userId);
        //     foreach (var application in applications)
        //     {
        //         var reminderDate = application.ApplicationDeadline.AddDays(-2);
        //         if (DateTime.Today == reminderDate)
        //         {
        //             var to = new EmailAddress(application.UserInfo.Email);
        //             var subject = "Application Deadline Reminder";
        //             var content = $"Don't forget to apply for the job by {application.ApplicationDeadline.ToShortDateString()}!";
        //             await SendEmail(to, subject, content, $"<strong>{content}</strong>");
        //         }
        //     }
        // }

        // public async Task SendFollowUpReminder(string userId)
        // {
        //     var applications = _applicationTrackerService.GetApplicationsByUserId(userId);
        //     foreach (var application in applications)
        //     {
        //         var reminderDate = application.AppliedDate.AddDays(7);
        //         if (DateTime.Today == reminderDate)
        //         {
        //             var to = new EmailAddress(application.UserInfo.Email);
        //             var subject = "Follow Up Reminder";
        //             var content = $"Don't forget to follow up on your {application.CompanyName} application submitted on {application.AppliedDate.ToShortDateString()}!";
        //             await SendEmail(to, subject, content, $"<strong>{content}</strong>");
        //         }
        //     }
        // }
    }
}