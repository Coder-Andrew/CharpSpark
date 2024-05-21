using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Concrete
{
    public class FollowerRepository : Repository<Follower>, IFollowerRepository
    {
        private readonly DbSet<Follower> _followers;
        public FollowerRepository(ResuMetaDbContext context) : base(context)
        {
            _followers = context.Followers;
        }
        public List<Follower> GetFollowersByProfileId(int? profileId) {
            if (profileId == null)
            {
                throw new Exception("Profile ID cannot be null");
            }
            List<Follower> followers = _followers.Where(f => f.ProfileId == profileId).ToList();
            return followers;
        }
        public List<Follower> GetFollowingByProfileId(int? profileId)
        {
            if (profileId == null)
            {
                throw new Exception("Profile ID cannot be null");
            }
            List<Follower> following = _followers.Where(f => f.FollowerId == profileId).ToList();
            return following;
        }

    }
}
