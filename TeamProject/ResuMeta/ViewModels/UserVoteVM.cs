using System.ComponentModel.DataAnnotations;

namespace ResuMeta.ViewModels
{
    public class UserVoteVM
    {
        public int? Id { get; set; }
        public int? ResumeId { get; set; }
        public int? UserInfoId { get; set; }
        public int? VoteId { get; set; }
    }
}