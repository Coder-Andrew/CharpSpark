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
    }
}