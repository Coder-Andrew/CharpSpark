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
                int resumeId = _resumeService.AddResumeInfo(response);
                string newUrl = "/Resume/ViewResume/" + resumeId;
                return Ok(new { Success = true, RedirectUrl = newUrl });
            }
            catch
            {
                return BadRequest();
            }
            //return Ok()
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

        // PUT: api/resume/{resumeId}
        [HttpPut("{resumeId}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult SaveResume(JsonElement content)
        {
            try
            {
                _resumeService.SaveResumeById(content);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/resume/{userId}
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ResumeVM>>> GetAllResumes(int userId)
        {
            try
            {
                var resumes = _resumeService.GetAllResumes(userId);
                if (resumes == null)
                {
                    return NotFound();
                }

                return Ok(resumes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}