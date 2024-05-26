using ResuMeta.Models;
using ResuMeta.Services.Abstract;
using System.Text.Json;

namespace ResuMeta.Services.Concrete
{
    public class FakeChatGPTService : IChatGPTService
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> AskQuestion(string question)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = $"This is a fake service used for testing the ChatGPT service, message: {question}"
            };
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> GenerateResume(int id, JsonElement jobDescription)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = $"This is a fake service used for testing the ChatGPT service, message: {id}"
            };
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> GenerateCoverLetter(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = $"This is a fake service used for testing the ChatGPT service, message: {id}"
            };
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> GenerateTailoredCoverLetter(int id, JsonElement jobDescription)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = $"This is a fake service used for testing the ChatGPT service, message: {id}"
            };
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ChatGPTResponse> GenerateCareerSuggestions(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return new ChatGPTResponse
            {
                Response = $"This is a fake service used for testing the ChatGPT service, message: {id}"
            };
        }
    }
}
