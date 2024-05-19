using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Concrete
{
    public class VoteRepository : Repository<UserVote>, IVoteRepository
    {
        private readonly DbSet<UserVote> _votes;
        private readonly ResuMetaDbContext _context;
        public VoteRepository(ResuMetaDbContext context) : base(context)
        {
            _votes = context.UserVotes;
            _context = context;
        }

        public List<UserVote> GetAllUpVotesByResumeId(int? resumeId) {
            if (resumeId == null)
            {
                return new List<UserVote>();
            }
            return _votes.Where(v => v.ResumeId == resumeId && v.Vote!.VoteValue == "UP").ToList();
        }
        public List<UserVote> GetAllDownVotesByResumeId(int? resumeId) {
            if (resumeId == null)
            {
                return new List<UserVote>();
            }
            return _votes.Where(v => v.ResumeId == resumeId && v.Vote!.VoteValue == "DOWN").ToList();
        }

        public async Task<int> AddOrUpdateVote(UserVoteVM vote)
        {
            if (vote.UserInfoId! == _context.Resumes.FirstOrDefault(r => r.Id == vote.ResumeId)!.UserInfoId)
            {
                throw new Exception("User cannot vote on their own resume");
            }
            UserVote? existingVote = _votes.FirstOrDefault(v => v.UserInfoId == vote.UserInfoId && v.ResumeId == vote.ResumeId);
            if (existingVote != null)
            {
                existingVote.VoteId = vote.VoteId;
                _votes.Update(existingVote);
                await _context.SaveChangesAsync();
                return existingVote.Id;
            }
            else
            {
                UserVote newVote = new UserVote
                {
                    UserInfoId = vote.UserInfoId,
                    ResumeId = vote.ResumeId,
                    VoteId = vote.VoteId
                };
                _votes.Add(newVote);
                await _context.SaveChangesAsync();
                UserVote addedVote = _votes.FirstOrDefault(v => v.UserInfoId == vote.UserInfoId && v.ResumeId == vote.ResumeId)!;
                return addedVote.Id;
            }
        }

        public int GetVoteIdByValue(string voteValue)
        {
            Vote? vote = _context.Votes.FirstOrDefault(v => v.VoteValue == voteValue);
            if (vote == null)
            {
                throw new Exception("Vote not found");
            }
            return vote.Id;
        }
    }
}
