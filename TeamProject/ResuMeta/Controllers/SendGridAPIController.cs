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

namespace ResuMeta.Controllers
{
    [Route("api/sendgrid/")]
    [ApiController]
    public class SendGridApiController : ControllerBase
    {
        private readonly ILogger<SendGridApiController> _logger;
        private readonly ISendGridService _sendGridService;
        private readonly IRecurringJobManager _recurringJobManager;
        public SendGridApiController(ILogger<SendGridApiController> logger, ISendGridService sendGridService, IRecurringJobManager recurringJobManager)
        {
            _logger = logger;
            _sendGridService = sendGridService;
            _recurringJobManager = recurringJobManager;
        }

        [HttpPost("{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetReminder(string userid)
        {
            try
            {

                //_recurringJobManager.AddOrUpdate("Job1", () => _sendGridService.TestReminder2(userid), "* * * * *");
               _sendGridService.TestReminder2(userid);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in SetReminder");
                return BadRequest("Error Setting Reminder");
            }

            // Send application deadline reminder
        // await SendGridService.SendApplicationDeadlineReminder(emailAddress, applicationDate);
            
            // Send follow up reminder
        // await SendGridService.SendFollowUpReminder(emailAddress, appliedDate);

        }
    }
}
