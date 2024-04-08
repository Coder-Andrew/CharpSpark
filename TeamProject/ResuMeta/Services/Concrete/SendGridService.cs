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
        //private static readonly EmailAddress from = new EmailAddress("charpspark@gmail.com", "ResuMeta");

        public SendGridService(
        ILogger<SendGridService> logger,
        UserManager<ApplicationUser> userManager,
        IRepository<UserInfo> userInfo,
        IRepository<ApplicationTracker> applicationTracker,
        IApplicationTrackerService applicationTrackerService,
        IConfiguration configuration
        )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _applicationTracker = applicationTracker;
            _applicationTrackerService = applicationTrackerService;
            _configuration = configuration;
        }


        public async Task TestReminder2(int userId)
        {
            var _apiKey = _configuration["SendGridApiKey"] ?? throw new InvalidOperationException("Connection string 'SendGridApiKey' not found.");
            var applications = _applicationTrackerService.GetApplicationsByUserId(userId);
            foreach (var application in applications)
            {
             var client = new SendGridClient(_apiKey);
             var from = new EmailAddress("charpspark@gmail.com", "ResuMeta");
            var subject = $"Application for {application.CompanyName}";
            // var to = new EmailAddress(application.UserInfo.Email);
            var to = new EmailAddress("daniellecampbell688@gmail.com", "Danielle");
            var plainTextContent = "Hello, World!";
            var htmlContent = "<strong>Hello, World!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
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

        // public async Task SendApplicationDeadlineReminder(EmailAddress to, DateTime applicationDate)
        // {
        //     var reminderDate = applicationDate.AddDays(-2);
        //     if (DateTime.Today == reminderDate)
        //     {
        //         var subject = "Application Deadline Reminder";
        //         var content = "Hello";
        //         //var content = $"Don't forget to apply for the job by {applicationDate.ToShortDateString()}!";
        //         await SendEmail(to, subject, content, $"<strong>{content}</strong>");
        //     }
        // }

        // public async Task SendFollowUpReminder(EmailAddress to, DateTime appliedDate)
        // {
        //     var reminderDate = appliedDate.AddDays(7);
        //     if (DateTime.Today == reminderDate)
        //     {
        //         var subject = "Follow Up Reminder";
        //         var content = "Hello";
        //         //var content = $"Don't forget to follow up on your {CompanyName} application submitted on {appliedDate.ToShortDateString()}!";
        //         await SendEmail(to, subject, content, $"<strong>{content}</strong>");
        //     }
        // }
    }
}