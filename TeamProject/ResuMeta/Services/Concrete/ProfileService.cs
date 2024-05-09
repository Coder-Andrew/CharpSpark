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
    // class JsonUserInfo
    // {
    //     public string? id { get; set; }
    //     public string? firstName { get; set; }
    //     public string? lastName { get; set; }
    //     public string? userName { get; set; }
    //     public string? profilePicturePath { get; set; }
    // }
    // class JsonProfile
    // {
    //     public string? id { get; set; }
    //     public string? description { get; set; }
    //     public JsonUserInfo? user { get; set; }
    //     public string? resume { get; set; }
    // }

    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ProfileService> _logger;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IResumeRepository _resumeRepo;
        private readonly IProfileRepository _profileRepo;
        private readonly IRepository<Profile> _profileRepository;
        public ProfileService(
            ILogger<ProfileService> logger,
            UserManager<ApplicationUser> userManager,
            IRepository<UserInfo> userInfo,
            IResumeRepository resumeRepo,
            IProfileRepository profileRepo,
            IRepository<Profile> profileRepository
            )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _resumeRepo = resumeRepo;
            _profileRepo = profileRepo;
            _profileRepository = profileRepository;
        }

        // public int AddProfile(JsonElement response)
        // {
        //     JsonSerializerOptions options = new JsonSerializerOptions
        //     {
        //         PropertyNameCaseInsensitive = true,
        //     };
        //     try
        //     {
        //         JsonProfile profile = JsonSerializer.Deserialize<JsonProfile>(response, options)!;
        //         if (profile.user == null)
        //         {
        //             throw new Exception("Invalid input");
        //         }
        //         Profile userProfile = _profileRepository.AddOrUpdate(new Profile { UserInfoId = Int32.Parse(profile.user.id!), Description = profile.description, Resume = profile.resume });
        //         return userProfile.Id;
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.LogError(e, "Error deserializing json");
        //         throw new Exception("Error deserializing json");
        //     }
        // }
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
            int profileId = userProfile!.Id;
            if (userProfile == null)
            {
                return false;
            }
            try
            {
                userProfile.Resume = profile.Resume;
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
            List<Profile> profiles = _profileRepository.GetAll().Where(x => x.UserInfo!.Email!.Contains(keyWord) || x.UserInfo!.FirstName!.Contains(keyWord) || x.UserInfo!.LastName!.Contains(keyWord)).ToList();
            List<Profile> topProfiles = profiles.GetRange(0, Math.Min(5, profiles.Count));
            List<ProfileVM> profileVMs = new List<ProfileVM>();
            foreach (Profile profile in topProfiles)
            {
                ProfileVM profileVM = await GetProfile(profile.Id);
                profileVMs.Add(profileVM);
            }
            return profileVMs;
        }
    }
}