using ResuMeta.Models;
using ResuMeta.Services.Abstract;
using System.Text.Json;

namespace ResuMeta.Services.Concrete
{
    public class FakeChatGPTService : IChatGPTService
    {
        private static string DefaultResponse(string message) => $"This is a fake service used for testing the ChatGPT service, please reach out to contact@andrewjesch.com if you'd like access to actual api, message: {message}";
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> AskQuestion(string question)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = DefaultResponse(question)
            };
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> GenerateResume(int id, JsonElement jobDescription)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = DefaultResponse(id.ToString())
            };
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> GenerateCoverLetter(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = DefaultResponse(id.ToString())
            };
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> GenerateTailoredCoverLetter(int id, JsonElement jobDescription)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = DefaultResponse(id.ToString())
            };
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> GenerateCareerSuggestions(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = DefaultResponse(id.ToString())
            };
        }
    }
}
