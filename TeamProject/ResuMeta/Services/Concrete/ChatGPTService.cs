using ResuMeta.Models;
using ResuMeta.Services.Abstract;
using System.Text.Json;

namespace ResuMeta.Services.Concrete
{
    class JsonMessage
    {
        public string Model { get; set; }
        public List<Message> Messages { get; set; }
    }

    class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

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
            JsonMessage jsonMessage = new JsonMessage
            {
                Model = "gpt-3.5-turbo",
                Messages = new List<Message>
                {
                    new Message
                    {
                        Role = "system",
                        Content = "You are to translate prompts from any language to French. Follow the format: (Original Langauage name): (user message)\nFrench Translation: (french translation)"
                    },
                    new Message
                    {
                        Role = "user",
                        Content = question
                    }
                }
            };
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string message = JsonSerializer.Serialize<string>(jsonMessage, options);
        }
    }
}
