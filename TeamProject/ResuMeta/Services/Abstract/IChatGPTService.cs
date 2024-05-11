using ResuMeta.Models;
using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface IChatGPTService
    {
        Task<ChatGPTResponse> AskQuestion(string question);
        Task<ChatGPTResponse> GenerateResume(int id, JsonElement jobDescription);
        Task<ChatGPTResponse> GenerateCoverLetter(int id);
    }
}
