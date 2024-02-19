namespace ResuMeta.ViewModels
{
    public class ResumeVM
    {
        public int ResumeId { get; set; }
        public EducationVM Education { get; set; }
        public DegreeVM Degree { get; set; }
        public List<SkillVM> Skills { get; set; }

    }
}
