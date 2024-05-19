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
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProfileService> _logger;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IProfileRepository _profileRepo;
        private readonly IRepository<Profile> _profileRepository;
        private readonly IVoteRepository _voteRepository;
        public ProfileService(
            ILogger<ProfileService> logger,
            UserManager<ApplicationUser> userManager,
            IRepository<UserInfo> userInfo,
            IProfileRepository profileRepo,
            IRepository<Profile> profileRepository,
            IVoteRepository voteRepository
            )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _profileRepo = profileRepo;
            _profileRepository = profileRepository;
            _voteRepository = voteRepository;
        }

        public async Task<ProfileVM> GetProfile(int profileId)
        {
            Profile? profile = _profileRepository.FindById(profileId);
            if (profile == null)
            {
                throw new Exception("Profile not found");
            }
            UserInfo? userInfo = _userInfo.GetAll().Where(x => x.Id == profile.UserInfoId).FirstOrDefault();
            if (userInfo == null)
            {
                throw new Exception("User not found");
            }
            var appUser = await _userManager.FindByIdAsync(userInfo.AspnetIdentityId!);
            if (appUser == null)
            {
                throw new Exception("User not found");
            }
            ProfileVM userProfile = _profileRepo.GetProfileById(userInfo.Id, appUser.Email!, userInfo.FirstName!, userInfo.LastName!, userInfo.ProfilePicturePath!);
            var upVotes = _voteRepository.GetAllUpVotesByResumeId(profile.ResumeId);
            var downVotes = _voteRepository.GetAllDownVotesByResumeId(profile.ResumeId);
            if (upVotes != null && upVotes.Count > 0)
            {
                userProfile.UpVoteCount = upVotes.Count;
            }
            else {
                userProfile.UpVoteCount = 0;
            }
            if (downVotes != null && downVotes.Count > 0)
            {
                userProfile.DownVoteCount = downVotes.Count;
            }
            else {
                userProfile.DownVoteCount = 0;
            }
            return userProfile;
        }

        public bool SaveProfile(int userId, ProfileVM profile)
        {
            UserInfo? currUser = _userInfo.FindById(userId);
            if (currUser == null)
            {
                return false;
            }
            Profile? userProfile = _profileRepository.GetAll().Where(x => x.UserInfoId == currUser.Id).FirstOrDefault();
            if (userProfile == null)
            {
                return false;
            }
            try
            {
                userProfile.ResumeHtml = profile.Resume;
                userProfile.ResumeId = profile.ResumeId;
                userProfile.Description = profile.Description;
                _profileRepository.AddOrUpdate(userProfile);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error saving profile");
                return false;
            
            }
        }

        public async Task<List<ProfileVM>> SearchProfile(string keyWord)
        {
            List<Profile> profiles = _profileRepository.GetAll().Where(x => x.UserInfo!.Email!.ToLower().Contains(keyWord.ToLower()) || x.UserInfo!.FirstName!.ToLower().Contains(keyWord.ToLower()) || x.UserInfo!.LastName!.ToLower().Contains(keyWord.ToLower()) || (x.UserInfo!.FirstName!.ToLower() + " " + x.UserInfo!.LastName!.ToLower()).Contains(keyWord.ToLower())).ToList();
            List<Profile> topProfiles = profiles.GetRange(0, Math.Min(5, profiles.Count));
            List<ProfileVM> profileVMs = new List<ProfileVM>();
            foreach (Profile profile in topProfiles)
            {
                try{
                    ProfileVM profileVM = await GetProfile(profile.Id);
                    profileVMs.Add(profileVM);
                }
                catch(Exception){
                    continue;
                }
            }
            return profileVMs;
        }
    }
}