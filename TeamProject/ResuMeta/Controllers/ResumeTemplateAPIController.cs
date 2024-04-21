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
    [Route("api/resumetemplate/")]
    [ApiController]
    public class ResumeTemplateAPIController : ControllerBase
    {
        private readonly IResumeTemplateService _resumeTemplateService;
        
        public ResumeTemplateAPIController(IResumeTemplateService resumeTemplateService)
        {
            _resumeTemplateService = resumeTemplateService;
        }

        // GET: api/resumetemplate/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ResumeVM> GetResumeTemplate(int id)
        {
            try
            {
                var resumeTemplate = _resumeTemplateService.GetResumeTemplateHtml(id);
                return Ok(resumeTemplate);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/resumetemplate
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<ResumeVM>> GetAllResumeTemplates()
        {
            try
            {
                var resumeTemplates = _resumeTemplateService.GetAllResumeTemplates();
                return Ok(resumeTemplates);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}