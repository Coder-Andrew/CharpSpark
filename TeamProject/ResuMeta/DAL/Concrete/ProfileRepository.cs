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
                Resume = profile.Resume,
                Description = profile.Description,
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                ProfilePicturePath = profilePicturePath
            };
        }
    }
}
