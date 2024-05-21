using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Abstract
{
    public interface IFollowerRepository : IRepository<Follower>
    {
        List<Follower> GetFollowersByProfileId(int? profileId);
        List<Follower> GetFollowingByProfileId(int? profileId);
    }
}
