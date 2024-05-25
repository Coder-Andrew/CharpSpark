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
        private readonly IFollowerRepository _followerRepo;
        public ProfileService(
            ILogger<ProfileService> logger,
            UserManager<ApplicationUser> userManager,
            IRepository<UserInfo> userInfo,
            IProfileRepository profileRepo,
            IRepository<Profile> profileRepository,
            IVoteRepository voteRepository,
            IFollowerRepository followerRepo
            )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _profileRepo = profileRepo;
            _profileRepository = profileRepository;
            _voteRepository = voteRepository;
            _followerRepo = followerRepo;
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
            var followerCount = _followerRepo.GetFollowersByProfileId(profileId).Count;
            var FollowingCount = _followerRepo.GetFollowingByProfileId(profileId).Count;
            userProfile.FollowerCount = followerCount;
            userProfile.FollowingCount = FollowingCount;
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


        public void UpdateTrendingProfiles()
        {
            // Define weights
            const float w_upvotes = 1.0F;
            const float cw_downvotes = 1.0F;
            const float w_days_since_upvote = 0.1F;
            const float w_days_since_downvote = 0.1F;
            const float w_followers = 1.5F;
            const float w_following = 0.5F;
            const float w_days_since_follower = 0.05F;
            const float w_days_since_following = 0.05F;
            const float w_resume_sections = 2.0F;

            DateTime now = DateTime.Now;
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            List<Profile> profiles = _profileRepository.GetAll().ToList();

            foreach (Profile profile in profiles)
            {
                ProfileVM2 userProfile = _profileRepo.GetProfileById(profile.Id);

                // get most recent upvote and downvote
                DateTime sinceLastupVote = _voteRepository.GetAllUpVotesByResumeId(profile.ResumeId)
                    .OrderByDescending(uv => uv.Timestamp)
                    .FirstOrDefault()
                    ?.Timestamp ?? epoch;

                DateTime sinceLastDownVote = _voteRepository.GetAllUpVotesByResumeId(profile.ResumeId)
                    .OrderByDescending(dv => dv.Timestamp)
                    .FirstOrDefault()
                    ?.Timestamp ?? now;

                // get most recent follower
                DateTime sinceLastFollower = _followerRepo.GetFollowersByProfileId(profile.Id)
                    .OrderByDescending(f => f.Timestamp)
                    .FirstOrDefault()
                    ?.Timestamp ?? epoch;

                DateTime sinceLastFollowing = _followerRepo.GetFollowingByProfileId(profile.Id)
                    .OrderByDescending(f => f.Timestamp)
                    .FirstOrDefault()
                    ?.Timestamp ?? epoch;

                // get counts
                int upvotes = userProfile.UpVoteCount ?? 0;
                int downvotes = userProfile.DownVoteCount ?? 0;
                int followers = userProfile.FollowerCount ?? 0;
                int following = userProfile.FollowingCount ?? 0;
                int resumeSections = userProfile.ResumeSections ?? 0;


                // get days since last action
                int daysSinceLastUpvote    = (int)(now - sinceLastupVote).TotalDays;
                int daysSinceLastDownvote  = (int)(now - sinceLastDownVote).TotalDays;
                int daysSinceLastFollower  = (int)(now - sinceLastFollower).TotalDays;
                int daysSinceLastFollowing = (int)(now - sinceLastFollowing).TotalDays;

                float net_score = (w_upvotes * userProfile.UpVoteCount - cw_downvotes * userProfile.DownVoteCount) ?? 1F;

                // recency factors
                double recency_upvote = Math.Exp(-w_days_since_upvote * daysSinceLastUpvote);
                double recency_downvote = 1 + w_days_since_downvote * daysSinceLastDownvote;
                double recency_follower = followers > 0 ? Math.Exp(-w_days_since_follower * daysSinceLastFollower) : 0;
                double recency_following = following > 0 ? Math.Exp(-w_days_since_following * daysSinceLastFollowing) : 0;

                // calculate followers and following scores
                double followers_score = followers > 0 ? w_followers * followers * recency_follower : -5;
                double following_score = following > 0 ? w_following * following * recency_following : -5;

                // resume sections score
                float resume_score = resumeSections > 0 ? w_resume_sections * resumeSections : -2;

                // base score
                float base_score = 1;

                // calculate final score
                double final_score = (net_score * recency_upvote * recency_downvote) +
                                     followers_score +
                                     following_score +
                                     resume_score +
                                     base_score;

                profile.ProfileScore = (int)final_score;
                _profileRepository.AddOrUpdate(profile);
            }
        }

        public async Task<List<ProfileVM2>> GetTrendingProfiles()
        {
            //    List<Profile> profiles = _profileRepository.GetAll().ToList();
            //    List<Profile> topProfiles = profiles.GetRange(0, Math.Min(5, profiles.Count));
            //    List<ProfileVM> profileVMs = new List<ProfileVM>();
            //    foreach (Profile profile in topProfiles)
            //    {
            //        try
            //        {
            //            ProfileVM profileVM = await GetProfile(profile.Id);
            //            profileVMs.Add(profileVM);
            //        }
            //        catch (Exception)
            //        {
            //            continue;
            //        }
            //    }
            //    return profileVMs;
            //}
            List<Profile> profiles = _profileRepository
                .GetAll()
                .OrderByDescending(p => p.ProfileScore)
                .ToList();

            List<ProfileVM2> profileViewModels = new List<ProfileVM2>();

            foreach (Profile profile in profiles)
            {
                profileViewModels.Add(_profileRepo.GetProfileById(profile.Id));
            }


            return profileViewModels;
        }
    }    
}