using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResuMeta.Models;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using System.Text.Json;
using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;

namespace ResuMeta.Controllers
{
    [Route("api/applicationtracker/")]
    [ApiController]
    public class ApplicationTrackerApiController : ControllerBase
    {
        private readonly ILogger<ApplicationTrackerApiController> _logger;
        private readonly IApplicationTrackerService _applicationTrackerService;
        public ApplicationTrackerApiController(ILogger<ApplicationTrackerApiController> logger, IApplicationTrackerService applicationTrackerService)
        {
            _logger = logger;
            _applicationTrackerService = applicationTrackerService;
        }


        // GET: api/applicationtracker/{userId}
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ApplicationTrackerVM>> GetApplicationsByUserId(int userId)
        {
            try
            {
                var applications = _applicationTrackerService.GetApplicationsByUserId(userId);
                return Ok(applications);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in GetAllApplications");
                return BadRequest("Error Retrieving Applications");
            }
        }

        //POST: api/applicationtracker/applicationInfo
        [HttpPost("applicationInfo")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddApplication(JsonElement content)
        {
            try
            {
                _applicationTrackerService.AddApplication(content);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in AddApplication");
                return BadRequest("Error Adding Application");
            }
        }

        //DELETE: api/applicationtracker/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteApplication(int id)
        {
            try
            {
                _applicationTrackerService.DeleteApplication(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in DeleteApplication");
                return BadRequest("Error Deleting Application");
            }
        }
    }
}