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
    [Route("api/coverletter/")]
    [ApiController]
    public class CoverLetterAPIController : ControllerBase
    {
        private readonly ICoverLetterService _coverLetterService;
        
        public CoverLetterAPIController(ICoverLetterService coverLetterService)
        {
            _coverLetterService = coverLetterService;
        }

        // PUT: api/coverletter/info
        [HttpPut("info")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult SubmitCoverLetterInfo(JsonElement response)
        {
            try
            {
                int coverLetterId = _coverLetterService.AddCoverLetterInfo(response);
                string newUrl = "/CoverLetter/ViewCoverLetter/" + coverLetterId;
                return Ok(new { Success = true, RedirectUrl = newUrl });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}