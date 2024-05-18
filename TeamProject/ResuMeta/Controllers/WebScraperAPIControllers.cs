using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResuMeta.Models;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using System.Text.Json;
using ResuMeta.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ResuMeta.Controllers
{
    [Route("api/scraper/")]
    [ApiController]
    public class WebScraperApiController : ControllerBase
    {
        private readonly IWebScraperService _webScraperService;
        public WebScraperApiController(IWebScraperService webScraperService)
        {
            _webScraperService = webScraperService;
        }

        // GET: api/scraper/cached_listings
        [AllowAnonymous]
        //[HttpGet("cached_listings/{pageNum}")]
        [HttpGet("cached_listings")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListings([FromQuery] int pageNum, [FromQuery] string? jobTitle)
        {
            try
            {
                JobListingContainerVM listings = await _webScraperService.GetCachedListings(pageNum, jobTitle!);
                return Ok(listings);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/scraper/search_jobs
        [HttpGet("search_jobs/job_title={jobTitle}&city={city}&state={state}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchListings(string jobTitle, string city, string state)
        {
            try
            {
                List<JobListingVM> listings = await _webScraperService.SearchJobs(jobTitle, city, state);
                return Ok(listings);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/scraper/job_description
        [HttpPost("job_description")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetJobDescription([FromBody] JsonElement jsonUrl)
        {
            try
            {        
                JobDescriptionVM description = await _webScraperService.GetJobDescription(jsonUrl);
                return Ok(description);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}