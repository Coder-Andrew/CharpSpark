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
        private readonly IProfileViewsRepository _profileViewsRepo;
        private readonly IRepository<Profile> _profileRepo;
        private readonly IFollowerRepository _followerRepo;
        private readonly IFollowerService _followerService;

        public ProfileApiController(IProfileService profileService, ILogger<ProfileApiController> logger, IVoteRepository voteRepo, IUserInfoRepository userInfoRepo, IProfileViewsRepository profileViewsRepo, IRepository<Profile> profileRepo, IFollowerRepository followerRepo, IFollowerService followerService)
        {
            _profileService = profileService;
            _logger = logger;
            _voteRepo = voteRepo;
            _userInfoRepo = userInfoRepo;
            _profileViewsRepo = profileViewsRepo;
            _profileRepo = profileRepo;
            _followerRepo = followerRepo;
            _followerService = followerService;
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

        // PUT: api/profiles/updateViews/{profileId}
        [AllowAnonymous]
        [HttpPut("updateViews/{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateViews(string profileId)
        {
            if (int.TryParse(profileId, out int id))
            {
                try
                {
                    await _profileViewsRepo.UpdateViewCount(id);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error incrementing views on profile");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Profile ID");
            }
        }

        // PUT: api/profiles/follow/{profileId}
        [HttpPut("follow/{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult FollowProfile(string profileId)
        {
            if (int.TryParse(profileId, out int id))
            {
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    var userInfo = _userInfoRepo.GetAll().Where(ui => ui.AspnetIdentityId == userId).FirstOrDefault();
                    if (userInfo == null)
                    {
                        _logger.LogError("User not found");
                        return BadRequest();
                    }
                    var followee = _profileRepo.GetAll().Where(p => p.UserInfoId == userInfo.Id).FirstOrDefault();
                    if (followee == null)
                    {
                        _logger.LogError("Followee must have a profile");
                        return BadRequest();
                    }
                    _followerService.AddFollower(id, followee.Id);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error following profile");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Profile ID");
            }
        }

        // PUT: api/profiles/follow/{profileId}
        [HttpPut("unfollow/{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UnfollowProfile(string profileId)
        {
            if (int.TryParse(profileId, out int id))
            {
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    var userInfo = _userInfoRepo.GetAll().Where(ui => ui.AspnetIdentityId == userId).FirstOrDefault();
                    if (userInfo == null)
                    {
                        _logger.LogError("User not found");
                        return BadRequest();
                    }
                    var followee = _profileRepo.GetAll().Where(p => p.UserInfoId == userInfo.Id).FirstOrDefault();
                    if (followee == null)
                    {
                        _logger.LogError("Followee must have a profile");
                        return BadRequest();
                    }
                    _followerService.RemoveFollower(id, followee.Id);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error unfollowing profile");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Profile ID");
            }
        }

        // GET: api/profiles/isFollowing/{profileId}
        [AllowAnonymous]
        [HttpGet("isFollowing/{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult IsFollower(string profileId)
        {
            if (int.TryParse(profileId, out int id))
            {
                try
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                    if (userId == null)
                    {
                        _logger.LogError("User not found");
                        return BadRequest();
                    }
                    var userInfo = _userInfoRepo.GetAll().Where(ui => ui.AspnetIdentityId == userId).FirstOrDefault();
                    if (userInfo == null)
                    {
                        _logger.LogError("User not found");
                        return BadRequest();
                    }
                    var followee = _profileRepo.GetAll().Where(p => p.UserInfoId == userInfo.Id).FirstOrDefault();
                    if (followee == null)
                    {
                        _logger.LogError("Followee must have a profile");
                        return BadRequest();
                    }
                    if (followee.Id == id)
                    {
                        return Ok(1);
                    }
                    var followStatus = _followerRepo.GetFollowersByProfileId(id).Where(f => f.FollowerId == followee.Id).FirstOrDefault();
                    if (followStatus == null)
                    {
                        return Ok(false);
                    }
                    return Ok(true);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error testing if profile is already followed");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Profile ID");
            }
        }


        // GET: api/profiles/followers/{profileId}
        [AllowAnonymous]
        [HttpGet("followers/{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFollowers(string profileId)
        {
            if (int.TryParse(profileId, out int id))
            {
                try
                {
                    var followers = await _followerService.GetFollowersByProfileId(id);
                    return Ok(followers);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error getting followers");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Profile ID");
            }
        }

        // GET: api/profiles/following/{profileId}
        [AllowAnonymous]
        [HttpGet("following/{profileId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFollowing(string profileId)
        {
            if (int.TryParse(profileId, out int id))
            {
                try
                {
                    var followers = await _followerService.GetFollowingByProfileId(id);
                    return Ok(followers);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error getting following");
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Profile ID");
            }
        }


        //[AllowAnonymous]
        //[HttpGet("updateTrendingProfiles")]
    }
}