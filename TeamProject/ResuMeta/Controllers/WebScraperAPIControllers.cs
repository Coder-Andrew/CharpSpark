using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResuMeta.Models;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using System.Text.Json;
using ResuMeta.ViewModels;

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

        // PUT: api/scraper/cached_listings
        [HttpPut("cached_listings/{pageNum}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListings(int pageNum)
        {
            try
            {
                List<JobListingVM> listings = await _webScraperService.GetCachedListings(pageNum);
                return Ok(listings);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/scraper/search_jobs
        [HttpPut("search_jobs")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchListings([FromBody] JsonElement parameters)
        {
            try
            {
                var title = parameters.GetProperty("title");
                var city = parameters.GetProperty("city");
                var state = parameters.GetProperty("state");
                List<JobListingVM> listings = await _webScraperService.SearchJobs(title.ToString(), city.ToString(), state.ToString());
                return Ok(listings);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}