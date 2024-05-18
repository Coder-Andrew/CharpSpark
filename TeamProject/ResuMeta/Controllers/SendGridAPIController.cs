using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ResuMeta.Models;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using System.Text.Json;
using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using ResuMeta.Data;

namespace ResuMeta.Controllers
{
    [Route("api/sendgrid/")]
    [ApiController]
    public class SendGridApiController : ControllerBase
    {
        private readonly ILogger<SendGridApiController> _logger;
        private readonly ISendGridService _sendGridService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IRepository<ApplicationTracker> _repository;
        public SendGridApiController(ILogger<SendGridApiController> logger, ISendGridService sendGridService, IRecurringJobManager recurringJobManager, IRepository<ApplicationTracker> repository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _sendGridService = sendGridService;
            _recurringJobManager = recurringJobManager;
            _repository = repository;
            _userManager = userManager;
        }

        [HttpPost("apply")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  IActionResult SetReminder([FromBody] ReminderVM reminder)
        {
            if (reminder == null)
            {
                return BadRequest("Reminder is null.");
            }
            if (User == null)
            {
                return Unauthorized();
            }
            try
            {
                string currUserId = _userManager.GetUserId(User)!;
                var application = _repository.FindById(reminder.applicationTrackerId);
                if (application == null)
                {
                    return BadRequest("Application not found.");
                }

                if (!application.ApplicationDeadline.HasValue)
                {
                    return BadRequest("Application deadline not set.");
                }

                // Calculate the reminder date , if it is for the current day or the next day, it will not send reminder
                var reminderDate = application.ApplicationDeadline.Value.AddDays(-2);

                // Calculate the time until the reminder date
                TimeOnly currentTime = TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay);
                DateTime reminderDateTime = reminderDate.ToDateTime(currentTime);

                _logger.LogInformation($"Reminder DateTime: {reminderDateTime}");

                if (reminderDateTime < DateTime.UtcNow)
                {
                    _logger.LogInformation($"Reminder date is in the past. Reminder not scheduled.");
                    return BadRequest("Reminder date is in the past.");
                }

                BackgroundJob.Schedule(() => _sendGridService.SendEmailReminderToApply(reminder, currUserId), reminderDateTime);
                _logger.LogInformation("Reminder has been Scheduled");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in SetReminder");
                return BadRequest("Error Setting Reminder");
            }
        }

        [HttpPost("followup")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SetFollowUp([FromBody] ReminderVM reminder)
        {
            if (reminder == null)
            {
                return BadRequest("Reminder is null.");
            }
            if (User == null)
            {
                return Unauthorized();
            }
            try
            {
                string currUserId = _userManager.GetUserId(User)!;
                var application = _repository.FindById(reminder.applicationTrackerId);
                if (application == null)
                {
                    return BadRequest("Application not found.");
                }

                if (!application.AppliedDate.HasValue)
                {
                    return BadRequest("Applied date not set.");
                }

                var reminderDate = application.AppliedDate.Value.AddDays(7);

                TimeOnly currentTime = TimeOnly.FromTimeSpan(DateTime.Now.TimeOfDay);
                DateTime reminderDateTime = reminderDate.ToDateTime(currentTime);

                _logger.LogInformation($"Reminder Follow Up DateTime: {reminderDateTime}");

                if (reminderDateTime < DateTime.UtcNow)
                {
                    _logger.LogInformation($"Reminder Follow Up date is in the past. Reminder not scheduled.");
                    return BadRequest("Reminder Follow Up date is in the past.");
                }

                BackgroundJob.Schedule(() => _sendGridService.SendEmailReminderToFollowUp(reminder, currUserId), reminderDateTime);
                _logger.LogInformation("Follow Up Reminder has been Scheduled");
                return Ok();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in SetFollowUp");
                return BadRequest("Error Setting Follow Up");
            }
        }
    }
}
