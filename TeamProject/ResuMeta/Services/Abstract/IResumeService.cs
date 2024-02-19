using ResuMeta.Models.DTO;
using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface IResumeService
    {
        int AddResumeInfo(JsonElement resumeInfo);
        IEnumerable<SkillDTO> GetSkillsBySubstring(string skillsSubstring);
    }
}