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
        private readonly IRepository<UserInfo> _userInfo;
        public ResumeApiController(IResumeService resumeService, IRepository<UserInfo> userInfo)
        {
            _resumeService = resumeService;
            _userInfo = userInfo;
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

        // DELETE: api/resume/{resumeId}
        [HttpDelete("{resumeId}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteResume(int resumeId)
        {
            try
            {
                _resumeService.DeleteResume(resumeId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/resume/education/{userInfoId} 
        [HttpGet("education/{userInfoId}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<EducationVM>> GetEducation(int userInfoId)
        {
            // string currUserId =  _userManager.GetUserId(User)!;
            // var currUserInfo = _userInfo.FirstOrDefault(x => x.Id == userInfoId);
            // if (currUserInfo == null || currUserInfo.AspnetIdentityId != currUserId)
            // {
            //     return BadRequest();
            // }
            try
            {
                var education = _resumeService.GetEducationByUserInfoId(userInfoId);
                return Ok(education);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/resume/employment/{userInfoId}
        [HttpGet("employment/{userInfoId}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<EmploymentHistoryVM>> GetEmployment(int userInfoId)
        {
            try
            {
                var employment = _resumeService.GetEmploymentByUserInfoId(userInfoId);
                return Ok(employment);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/resume/achievements/{userInfoId}
        [HttpGet("achievements/{userInfoId}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<AchievementVM>> GetAchievements(int userInfoId)
        {
            try
            {
                var achievements = _resumeService.GetAchievementsByUserInfoId(userInfoId);
                return Ok(achievements);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/resume/projects/{userInfoId}
        [HttpGet("projects/{userInfoId}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProjectVM>> GetProjects(int userInfoId)
        {
            try
            {
                var projects = _resumeService.GetProjectsByUserInfoId(userInfoId);
                return Ok(projects);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}