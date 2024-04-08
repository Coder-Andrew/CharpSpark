using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public IActionResult SetReminder(int userid)
        {
            
            _recurringJobManager.AddOrUpdate("Job1", () => _sendGridService.TestReminder2(userid), "* * * * *");


            // Send application deadline reminder
        // await SendGridService.SendApplicationDeadlineReminder(emailAddress, applicationDate);
            
            // Send follow up reminder
        // await SendGridService.SendFollowUpReminder(emailAddress, appliedDate);
            
            return Ok();
        }
    }
}
