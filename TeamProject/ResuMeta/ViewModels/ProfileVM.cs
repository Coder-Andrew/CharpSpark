using System.ComponentModel.DataAnnotations;

namespace ResuMeta.ViewModels
{
    public class ProfileVM
    {
        public int? ProfileId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        [StringLength(250)]
        public string? Description { get; set; }
        public byte[]? ProfilePicturePath { get; set; }
        public string? Resume { get; set; }
        public int? ResumeId { get; set; }
        public int? UpVoteCount { get; set; }
        public int? DownVoteCount { get; set; }
        public int? FollowerCount { get; set; }
        public int? FollowingCount { get; set; }
    }
}