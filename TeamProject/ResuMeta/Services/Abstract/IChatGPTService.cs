using ResuMeta.Models;

namespace ResuMeta.Services.Abstract
{
    public interface IChatGPTService
    {
        Task<ChatGPTResponse> AskQuestion(string question);
        Task<ChatGPTResponse> GenerateResume(int id);
        Task<ChatGPTResponse> GenerateCoverLetter(int id);
    }
}
