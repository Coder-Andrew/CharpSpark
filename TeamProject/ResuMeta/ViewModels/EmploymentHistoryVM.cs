namespace ResuMeta.ViewModels
{
    public class EmploymentHistoryVM
    {
        public string? Company { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? JobTitle { get; set; }
        public DateOnly? StartDate { get; set; } 
        public DateOnly? EndDate { get; set; }
    }
}