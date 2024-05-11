using ResuMeta.Models;
using ResuMeta.Services.Abstract;
using System.Text.Json;

namespace ResuMeta.Services.Concrete
{
    public class FakeChatGPTService : IChatGPTService
    {
        public async Task<ChatGPTResponse> AskQuestion(string question)
        {
            return new ChatGPTResponse
            {
                Response = $"This is a fake service used for testing the ChatGPT service, message: {question}"
            };
        }
        public async Task<ChatGPTResponse> GenerateResume(int id, JsonElement jobDescription)
        {
            return new ChatGPTResponse
            {
                Response = $"This is a fake service used for testing the ChatGPT service, message: {id}"
            };
        }

        public async Task<ChatGPTResponse> GenerateCoverLetter(int id)
        {
            return new ChatGPTResponse
            {
                Response = $"This is a fake service used for testing the ChatGPT service, message: {id}"
            };
        }
    }
}
