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
using ResuMeta.DAL.Abstract;

namespace ResuMeta.Controllers
{

    [Route("api/profiles")]
    [ApiController]
    public class ProfileApiController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<ProfileApiController> _logger;
        private readonly IVoteRepository _voteRepo;
        private readonly IUserInfoRepository _userInfoRepo;

        public ProfileApiController(IProfileService profileService, ILogger<ProfileApiController> logger, IVoteRepository voteRepo, IUserInfoRepository userInfoRepo)
        {
            _profileService = profileService;
            _logger = logger;
            _voteRepo = voteRepo;
            _userInfoRepo = userInfoRepo;
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

        // PUT: api/profiles/vote/{profileId}
        [HttpPut("vote/{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VoteProfile(string profileId, [FromBody] JsonElement voteValue)
        {
            if (int.TryParse(profileId, out int id))
            {
                try
                {
                    var voteValueString = voteValue.GetProperty("voteValue").ToString();
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    var userInfoId = _userInfoRepo.GetAll().Where(ui => ui.AspnetIdentityId == userId).FirstOrDefault();
                    var profile = await _profileService.GetProfile(id);
                    var resumeId = profile.ResumeId!;
                    var voteId = _voteRepo.GetVoteIdByValue(voteValueString);
                    var vote = new UserVoteVM
                    {
                        UserInfoId = userInfoId!.Id,
                        ResumeId = resumeId,
                        VoteId = voteId
                    };
                    var result = await _voteRepo.AddOrUpdateVote(vote);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error voting on profile");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Profile ID");
            }
        }

    }
}