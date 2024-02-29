using ResuMeta.Models;
using ResuMeta.Services.Abstract;

namespace ResuMeta.Services.Concrete
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChatGPTService> _logger;
        public ChatGPTService(HttpClient httpClient, ILogger<ChatGPTService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public Task<ChatGPTResponse> AskQuestion(string question)
        {
            throw new NotImplementedException();
        }
    }
}
