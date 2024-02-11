using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface IResumeService
    {
        void AddResumeInfo(JsonElement resumeInfo);
    }
}