using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Abstract
{
    public interface IVoteRepository : IRepository<UserVote>
    {
        List<UserVote> GetAllUpVotesByResumeId(int? resumeId);
        List<UserVote> GetAllDownVotesByResumeId(int? resumeId);
        Task<int> AddOrUpdateVote(UserVoteVM vote);
        int GetVoteIdByValue(string voteValue);
    }
}
