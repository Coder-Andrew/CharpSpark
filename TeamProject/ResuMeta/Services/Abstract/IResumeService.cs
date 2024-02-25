using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface IResumeService
    {
        int AddResumeInfo(JsonElement resumeInfo);
        IEnumerable<SkillDTO> GetSkillsBySubstring(string skillsSubstring);
        ResumeVM GetResume(int resumeId);
        void SaveResumeById(JsonElement content);
        ResumeVM GetResumeHtml(int resumeId);
        List<KeyValuePair<int, string>> GetResumeIdList(int userId);
    }
}