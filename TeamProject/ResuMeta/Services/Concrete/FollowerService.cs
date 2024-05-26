using ResuMeta.Models;
using System.Text.Json;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using ResuMeta.Data;

namespace ResuMeta.Services.Concrete
{
    public class FollowerService : IFollowerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProfileService> _logger;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IProfileRepository _profileRepo;
        private readonly IRepository<Profile> _profileRepository;
        private readonly IFollowerRepository _followerRepository;
        private readonly IProfileService _profileService;

        public FollowerService(
            ILogger<ProfileService> logger,
            UserManager<ApplicationUser> userManager,
            IRepository<UserInfo> userInfo,
            IProfileRepository profileRepo,
            IRepository<Profile> profileRepository,
            IFollowerRepository followerRepository,
            IProfileService profileService
            )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _profileRepo = profileRepo;
            _profileRepository = profileRepository;
            _followerRepository = followerRepository;
            _profileService = profileService;
        }

        public async Task<List<ProfileVM>> GetFollowersByProfileId(int? profileId) {
            try {
                List<Follower> followers = _followerRepository.GetFollowersByProfileId(profileId);
                List<ProfileVM> profileVMs = new List<ProfileVM>();
                foreach (Follower follower in followers)
                {
                    try{
                        ProfileVM profileVM =  await _profileService.GetProfile((int)follower.FollowerId!);
                        profileVMs.Add(profileVM);
                    }
                    catch(Exception){
                        continue;
                    }
                }
                return profileVMs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting followers");
                throw new Exception("Error getting followers");
            }
        }

        public async Task<List<ProfileVM>> GetFollowingByProfileId(int? profileId)
        {
            try
            {
                List<Follower> following = _followerRepository.GetFollowingByProfileId(profileId);
                List<ProfileVM> profileVMs = new List<ProfileVM>();
                foreach (Follower follower in following)
                {
                    try
                    {
                        ProfileVM profileVM = await _profileService.GetProfile((int)follower.ProfileId!);
                        profileVMs.Add(profileVM);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                return profileVMs;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting following");
                throw new Exception("Error getting following");
            }
        }
        public void AddFollower(int? profileId, int? followerId) {
            try
            {
                if (profileId == followerId)
                {
                    throw new Exception("Cannot follow yourself");
                }
                Follower curr = _followerRepository.GetAll().Where(x => x.ProfileId == profileId && x.FollowerId == followerId).FirstOrDefault()!;
                if (curr != null)
                {
                    throw new Exception("Follower already exists");
                }
                Follower follower = new Follower { ProfileId = profileId, FollowerId = followerId };
                _followerRepository.AddOrUpdate(follower);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error adding follower");
                throw new Exception("Error adding follower");
            }
        }
        public void RemoveFollower(int? profileId, int? followerId)
        {
            try
            {
                if (profileId == followerId)
                {
                    throw new Exception("Cannot unfollow yourself");
                }
                Follower follower = _followerRepository.GetAll().Where(x => x.ProfileId == profileId && x.FollowerId == followerId).FirstOrDefault()!;
                if (follower == null)
                {
                    throw new Exception("Follower does not exist");
                }
                _followerRepository.Delete(follower);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error removing follower");
                throw new Exception("Error removing follower");
            } 
        }

        public int GetFollowerCount(int? profileId) {
            try
            {
                List<Follower> followers = _followerRepository.GetFollowersByProfileId(profileId);
                return followers.Count;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting follower count");
                throw new Exception("Error getting follower count");
            }
        }
        public int GetFollowingCount(int? profileId) {
            try
            {
                List<Follower> following = _followerRepository.GetFollowingByProfileId(profileId);
                return following.Count;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error getting following count");
                throw new Exception("Error getting following count");
            }
        }
    }
}