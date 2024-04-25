using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Abstract
{
    public interface IResumeTemplateRepository : IRepository<ResumeTemplate>
    {
        ResumeVM GetResumeTemplateHtml(int templateId);
        List<ResumeVM> GetAllResumeTemplates();
        ResumeVM ConvertResumeToTemplate(ResumeVM template, ResumeVM resume, UserInfo currUser);
    }
}
