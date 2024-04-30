namespace ResuMeta.ViewModels
{
    public class TemplateAndResumeVM
    {
        public ResumeVM? Resume { get; set; }
        public ResumeVM? Template { get; set; }
        public List<ResumeVM>? TemplatesList { get; set; }
    }
}