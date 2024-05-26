using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ResuMeta.Models;
using ResuMeta.Services.Abstract;
using System.Text.Json;

namespace ResuMeta.Controllers
{
    [Route("api/cgpt")]
    [ApiController]
    public class ChatGPTAPIController : ControllerBase
    {
        private readonly IChatGPTService _chatGPTService;
        private readonly ILogger<ChatGPTAPIController> _logger;
        public ChatGPTAPIController(IChatGPTService chatGPTService, ILogger<ChatGPTAPIController> logger)
        {
            _logger = logger;
            _chatGPTService = chatGPTService;
        }

        // POST: api/cgpt/ask
        [HttpPost("ask")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChatGPTResponse>> AskQuestion([FromBody] JsonElement jsonQuestion)
        {
            try
            {   
                string question = jsonQuestion.GetProperty("question").GetString()!;
                if (question.IsNullOrEmpty()) return BadRequest();

                var response = await _chatGPTService.AskQuestion(question);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }

         // POST: api/cgpt/improve/{id}
        [HttpPost("improve/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateResume(int id, [FromBody] JsonElement jobDescription)
        {
            try
            {   
                var response = await _chatGPTService.GenerateResume(id, jobDescription);

                if (response == null || response.Response == null)
                {
                    return BadRequest();
                }
                
                string responseData = response.Response.Trim('"');

                return Content(responseData, "text/html");
            }
            catch
            {
                return BadRequest();
            }
        }

         // POST: api/cgpt/improve-coverletter/{id}
        [HttpPost("improve-coverletter/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateCoverLetter(int id)
        {
            try
            {   
                var response = await _chatGPTService.GenerateCoverLetter(id);

                if (response == null || response.Response == null)
                {
                    return BadRequest();
                }
                
                string responseData = response.Response.Trim('"');

                return Content(responseData, "text/html");
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/cgpt/tailored-coverletter/{id}
        [HttpPost("tailored-coverletter/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateTailoredCoverLetter(int id, [FromBody] JsonElement jobDescription)
        {
            try
            {   
                var response = await _chatGPTService.GenerateTailoredCoverLetter(id, jobDescription);

                if (response == null || response.Response == null)
                {
                    return BadRequest();
                }
                
               string responseData = response.Response.Trim('"');

                return Content(responseData, "text/html");
            }
            catch
            {
                return BadRequest();
            }
        }




        // POST: api/cgpt/generatecareers/{id}
        [HttpPost("generatecareers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GenerateCareerSuggestions(int id)
        {
            try
            {   
                var response = await _chatGPTService.GenerateCareerSuggestions(id);

                if (response == null || response.Response == null)
                {
                    return BadRequest();
                }
                
                string responseData = response.Response.Trim('"');

                return Content(responseData, "text/html");
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
