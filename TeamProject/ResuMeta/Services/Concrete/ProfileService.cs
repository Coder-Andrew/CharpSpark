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
    class JsonUserInfo
    {
        public string? id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? userName { get; set; }
        public string? profilePicturePath { get; set; }
    }
    class JsonProfile
    {
        public string? id { get; set; }
        public string? description { get; set; }
        public JsonUserInfo? user { get; set; }
        public string? resume { get; set; }
    }

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

        public int AddProfile(JsonElement response)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            try
            {
                JsonProfile profile = JsonSerializer.Deserialize<JsonProfile>(response, options)!;
                if (profile.user == null)
                {
                    throw new Exception("Invalid input");
                }
                Profile userProfile = _profileRepository.AddOrUpdate(new Profile { UserInfoId = Int32.Parse(profile.user.id!), Description = profile.description, Resume = profile.resume });
                return userProfile.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
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
            return userProfile;
        }

        public void SaveProfileById(JsonElement content)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            try
            {
                JsonProfile profile = JsonSerializer.Deserialize<JsonProfile>(content, options)!;
                if (profile.user == null)
                {
                    throw new Exception("Invalid input");
                }
                Profile userProfile = _profileRepository.FindById(Int32.Parse(profile.user.id!));
                if (userProfile == null)
                {
                    throw new Exception("Profile not found");
                }
                userProfile.Description = profile.description;
                userProfile.Resume = profile.resume;
                _profileRepository.AddOrUpdate(userProfile);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
        }

    }
}