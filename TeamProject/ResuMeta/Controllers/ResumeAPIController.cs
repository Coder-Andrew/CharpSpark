using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResuMeta.Models;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using System.Text.Json;
using ResuMeta.Models.DTO;

namespace ResuMeta.Controllers
{
    [Route("api/resume/")]
    [ApiController]
    public class ResumeApiController : ControllerBase
    {
        private readonly IResumeService _resumeService;
        public ResumeApiController(IResumeService resumeService)
        {
            _resumeService = resumeService;
        }

        // PUT: api/resume/info
        [HttpPut("info")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult SubmitResumeInfo(JsonElement response)
        {
            try
            {
                _resumeService.AddResumeInfo(response);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        // GET: api/resume/skills/{skillSubstring}
        [HttpGet("skills/{skillSubstring}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<SkillDTO>> GetSkills(string skillSubstring)
        {
            try
            {
                var skills = _resumeService.GetSkillsBySubstring(skillSubstring);
                return Ok(skills);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}