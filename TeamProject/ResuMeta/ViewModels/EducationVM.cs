namespace ResuMeta.ViewModels
{
    public class EducationVM
    {
        public string? Institution { get; set; }
        public string? EducationSummary { get; set; }
        public DateOnly? StartDate { get; set; } 
        public DateOnly? EndDate { get; set; }
        public bool? Completion { get; set; }
    }
}
