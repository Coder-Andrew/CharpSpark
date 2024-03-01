using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResuMeta.Models;
using ResuMeta.Services.Abstract;

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
        public async Task<ActionResult<ChatGPTResponse>> AskQuestion([FromBody] string question)
        {
            try
            {
                var response = await _chatGPTService.AskQuestion(question);
                return Ok(response);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
