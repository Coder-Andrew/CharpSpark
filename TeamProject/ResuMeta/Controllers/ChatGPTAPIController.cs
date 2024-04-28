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
        public ChatGPTAPIController(IChatGPTService chatGPTService)
        {
            _chatGPTService = chatGPTService;
        }

        // POST: api/cgpt/ask
        [HttpPost("ask")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChatGPTResponse>> AskQuestion([FromBody] JsonElement jsonQuestion)
        {
            try
            {   
                string question = jsonQuestion.GetProperty("question").GetString();
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
        public async Task<ActionResult<ChatGPTResponse>> GenerateResume(int id)
        {
            try
            {   
                var response = await _chatGPTService.GenerateResume(id);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
