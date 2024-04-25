using ResuMeta.Models.DTO;
using ResuMeta.Models;
using System.Text.Json;
using ResuMeta.ViewModels;

namespace ResuMeta.Services.Abstract
{
    public interface IResumeTemplateService
    {
        ResumeVM GetResumeTemplateHtml(int templateId);
        List<ResumeVM> GetAllResumeTemplates();
        ResumeVM ConvertResumeToTemplate(ResumeVM template, ResumeVM resume, UserInfo currUser);
    }
}