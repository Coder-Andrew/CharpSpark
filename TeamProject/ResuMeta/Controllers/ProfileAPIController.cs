using ResuMeta.Models;
using ResuMeta.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResuMeta.Services.Abstract;
using System.Text.Json;
using System;
using System.Net;
using NuGet.Protocol;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ResuMeta.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ResuMeta.Controllers
{

    [Route("api/profiles")]
    [ApiController]
    public class ProfileApiController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<ProfileApiController> _logger;

        public ProfileApiController(IProfileService profileService, ILogger<ProfileApiController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }


        // GET: api/profiles/{id}
        [HttpGet("{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProfile(string profileId)
        {
            if (int.TryParse(profileId, out int id))
            {
                try
                {
                    var profile = await _profileService.GetProfile(id);
                    return Ok(profile);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error getting profile");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Profile ID");
            }
        }


        // GET: api/profiles/search/{keyWord}
        [AllowAnonymous]
        [HttpGet("search/{keyWord}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchProfile(string keyWord)
        {
            List<ProfileVM> profiles = await _profileService.SearchProfile(keyWord);
            if (profiles.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(profiles);
            }
        }


    }
}