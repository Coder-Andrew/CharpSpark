namespace ResuMeta.ViewModels
{
    public class JobListingVM
    {
        public string? Company { get; set; }
        public string? Link { get; set; }
        public string? Location { get; set; }
        public string? JobTitle { get; set; }
        public DateOnly? DateApplied { get; set; }
        public string? GoogleReCaptchaSiteKey { get; set; }
    }
}