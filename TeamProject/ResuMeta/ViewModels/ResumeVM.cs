namespace ResuMeta.ViewModels
{
    public class ResumeVM
    {
        public int? ResumeId { get; set; }
        public string? Title { get; set; }
        public string? HtmlContent { get; set; }
        public EducationVM? Education { get; set; }
        public DegreeVM? Degree { get; set; }
        public List<SkillVM>? Skills { get; set; }

    }
}
