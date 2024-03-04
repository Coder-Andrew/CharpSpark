using ResuMeta.Models;
using ResuMeta.Services.Abstract;

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
    }
}
