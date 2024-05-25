using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Concrete
{
    public class ProfileRepository : Repository<ProfileVM>, IProfileRepository
    {
        private readonly DbSet<Profile> _profiles;
        public ProfileRepository(ResuMetaDbContext context) : base(context)
        {
            _profiles = context.Profiles;
        }

        public ProfileVM GetProfileById(int userId, string userName, string firstName, string lastName, byte[] profilePicturePath)
        {
            Profile? profile = _profiles.FirstOrDefault(p => p.UserInfoId == userId);
            if (profile == null)
            {
                throw new Exception("Profile not found");
            }
            return new ProfileVM
            {
                ProfileId = profile.Id,
                Resume = profile.ResumeHtml,
                ResumeId = profile.ResumeId,
                Description = profile.Description,
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicturePath = profilePicturePath,
                ProfileScore = profile.ProfileScore,
            };
        }
        public ProfileVM2 GetProfileById(int userId)
        {
            Profile? profile = _profiles.FirstOrDefault(p => p.UserInfoId == userId);
            if (profile == null)
            {
                throw new Exception("Profile not found");
            }
            return new ProfileVM2
            {
                ProfileId = profile.Id,
                Resume = profile.ResumeHtml,
                ResumeId = profile.ResumeId,
                Description = profile.Description,
                FirstName = profile.UserInfo?.FirstName ?? "N/A",
                LastName = profile.UserInfo?.LastName ?? "N/A",
                ProfilePicturePath = profile.UserInfo?.ProfilePicturePath,
                ProfileScore = profile.ProfileScore,
                UpVoteCount = profile.Resume?.UserVotes.Count(v => v.VoteId == 1) ?? 0,
                DownVoteCount = profile.Resume?.UserVotes.Count(v => v.VoteId == 2) ?? 0,
                FollowerCount = profile.Followers.Count,
                FollowingCount = profile.Following.Count,
                ResumeSections = profile.Resume?.Achievements.Count ?? 0 + 
                    profile.Resume?.UserSkills.Count ?? 0 + 
                    profile.Resume?.Educations.Count ?? 0 +
                    profile.Resume?.Projects.Count ?? 0 +
                    profile.Resume?.EmploymentHistories.Count ?? 0,
                ViewCount = profile.ProfileViews.ViewCount ?? 0,
            };
        }
    }
}
