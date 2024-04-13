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
    class JsonSendGridService
    {
        public int? userId { get; set; }
        public int? applicationTrackerId { get; set; }
    }
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


        public async Task SendEmailReminderToApply(ReminderVM reminder, string currUserId)
        {
            var userId = reminder.userId;
            var applicationTrackerId = reminder.applicationTrackerId;
            var applications = _applicationTrackerService.GetApplicationsByUserId(userId);
            var id = currUserId;
            var emailFromAddress = _configuration["SendFromEmail"];
            UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
            _logger.LogInformation($"User with email {currUser.Email}  found. ");
            string email = currUser.Email;

            var application = applications.FirstOrDefault(a => a.ApplicationTrackerId == applicationTrackerId);
            if (application != null && application.ApplicationDeadline.HasValue)
            {
                var from = new EmailAddress(emailFromAddress, "ResuMeta");
                var subject = $"Application for {application.CompanyName}";
                var to = new EmailAddress(email, currUser.FirstName);
                var plainTextContent = $"Don't forget to apply for the {application.JobTitle} position at {application.CompanyName} before {application.ApplicationDeadline}!";
                var htmlContent = $"<strong>Don't forget to apply for the {application.JobTitle} position at {application.CompanyName} before {application.ApplicationDeadline}!</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await _client.SendEmailAsync(msg);
                _logger.LogInformation($"Email sent to {email}");
            }
        }

        public async Task SendEmailReminderToFollowUp(ReminderVM reminder, string currUserId)
        {
            var userId = reminder.userId;
            var applicationTrackerId = reminder.applicationTrackerId;
            var applications = _applicationTrackerService.GetApplicationsByUserId(userId);
            var id = currUserId;
             var emailFromAddress = _configuration["SendFromEmail"];
            UserInfo currUser = _userInfo.GetAll().Where(x => x.AspnetIdentityId == id).FirstOrDefault()!;
            _logger.LogInformation($"User with email {currUser.Email}  found. ");
            string email = currUser.Email;

            var application = applications.FirstOrDefault(a => a.ApplicationTrackerId == applicationTrackerId);

            if (application != null && application.ApplicationDeadline.HasValue)
            {
                var from = new EmailAddress(emailFromAddress, "ResuMeta");
                var subject = $"Follow up for {application.CompanyName}";
                var to = new EmailAddress(email, currUser.FirstName);
                var plainTextContent = $"Don't forget to follow up with the hiring manager for the {application.JobTitle} position at {application.CompanyName}. As a reminder, you applied on {application.AppliedDate}.";
                var htmlContent = $"<strong>Don't forget to follow up with the hiring manager for the {application.JobTitle} position at {application.CompanyName}. As a reminder, you applied on {application.AppliedDate}.</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await _client.SendEmailAsync(msg);
                _logger.LogInformation($"Email sent to {email}");
            }
        }
    }
}