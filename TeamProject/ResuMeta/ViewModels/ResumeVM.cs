namespace ResuMeta.ViewModels
{
    public class ResumeVM
    {
        public int? ResumeId { get; set; }
        public string? Title { get; set; }
        public string? HtmlContent { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Summary { get; set; }
        public List<EducationVM>? Education { get; set; }
        public List<DegreeVM>? Degree { get; set; }
        public List<SkillVM>? Skills { get; set; }
        public List<AchievementVM>? Achievements { get; set; }
        public List<ProjectVM>? Projects { get; set; }
        public List<EmploymentHistoryVM>? EmploymentHistory { get; set; }
        public List<ReferenceContactInfoVM>? ReferenceContactInfo { get; set; }

    }
}
