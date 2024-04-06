namespace ResuMeta.ViewModels
{
    public class ApplicationTrackerVM
    {
        public int ApplicationTrackerId { get; set; }
        public int? UserInfoId { get; set; }
        public string? JobTitle { get; set; }
        public string? CompanyName { get; set; }
        public string? JobListingUrl { get; set; }
        public DateOnly? AppliedDate { get; set; }
        public DateOnly? ApplicationDeadline { get; set; }
        public string? Status { get; set; }
        public string? Notes { get; set; }
    }
}