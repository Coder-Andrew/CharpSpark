using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Abstract
{
    public interface IResumeRepository : IRepository<ResumeVM>
    {
        List<ResumeVM> GetAllResumes(int userId);
        List<KeyValuePair<int, string>> GetResumeIdList(int userId);
        ResumeVM GetResume(int resumeId, string userEmail);
        ResumeVM GetResumeHtml(int resumeId);
    }
}
