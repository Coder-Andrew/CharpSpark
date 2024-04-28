using ResuMeta.Models;
using ResuMeta.Services.Abstract;
using System;
using System.Text;
using System.Net;
using System.Text.Json;
using ResuMeta.DAL.Abstract;

namespace ResuMeta.Services.Concrete
{
    class JsonMessage
    {
        public string model { get; set; }
        public List<Message> messages { get; set; }
    }

    class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    class Choice
    {
        public Message message { get; set; }
    }
    class CGPTResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChatGPTService> _logger;
        private readonly IResumeRepository _resumeRepository;
        public ChatGPTService(HttpClient httpClient, ILogger<ChatGPTService> logger, IResumeRepository resumeRepository)
        {
            _httpClient = httpClient;
            _logger = logger;
            _resumeRepository = resumeRepository;
        }

        public async Task<ChatGPTResponse> AskQuestion(string question)
        {
            JsonMessage jsonMessage = new JsonMessage
            {
                model = "gpt-3.5-turbo",
                messages = new List<Message>
                {
                    new Message
                    {
                        role = "system",
                        content = "You are here to answer questions about ResuMeta, an AI-enabled platform for resume creation and career advice. " +
                            "ResuMeta is a web application developed by the CharpSpark team at Western Oregon University. " +
                            "It uses ASP.NET Core MVC, SQL Server, Azure, C#, JavaScript, HTML/CSS, and other technologies. " +
                            "Our goal is to simplify the resume creation process and help users get their resumes noticed by employers. " +
                            "We offer a free resume enhancement suite that allows users to submit their current resume for feedback, build new resumes based on AI and industry standards, and edit them as needed. " +
                            "There are pages for creating resumes (tab named Resumes), viewing resumes (Your dashboard), and learning about the project accessable in the nav bar (about)" +
                            "Please ONLY answer questions related to careers, resume creation, and our project."
                    },
                    new Message
                    {
                        role = "user",
                        content = question
                    }
                }
            };
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string message = JsonSerializer.Serialize<JsonMessage>(jsonMessage, options);

            
            StringContent postContent = new StringContent(message, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("v1/chat/completions", postContent);

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error: {response.StatusCode} - {content}");
                throw new Exception($"Error: {response.StatusCode} - {content}");
            }

            CGPTResponse cGPTResponse = JsonSerializer.Deserialize<CGPTResponse>(content, options);

            return new ChatGPTResponse
            {
                Response = cGPTResponse.choices[0].message.content
            };

        }

        public async Task<ChatGPTResponse> GenerateResume(int id)
        {
            var resumeContent = _resumeRepository.GetResumeHtml(id);
            var htmlContent = resumeContent.HtmlContent;
            var decodedHtmlContent = WebUtility.UrlDecode(htmlContent);
            JsonMessage jsonMessage = new JsonMessage
            {
                model = "gpt-3.5-turbo",
                messages = new List<Message>
                {
                    new Message
                    {
                        role = "system",
                        content = "You are here to improve a resume. " +
                            "You need to correct any grammatical errors, make the language more professional, " +
                            "and optimize the content for job applications. " +
                            "Please return the improved content in the same HTML format that I'm sending, " +
                            "without adding any extra code or HTML headers. " +
                            "Here is the resume: "
                    },
                    // new Message
                    // {
                    //     role = "system",
                    //     content = "You are here to improve a resume. " +
                    //         "You need to improve the content resume and send it back to me in the same html format (html tags). " +
                    //         "Do not include any extra code other than the same types of html tags I am sending.  " +
                    //         "Please do not add any html headers, only return the same kinds of html tags that are sent with the resume.  " +
                    //         "Here is the resume: " 
                    // },
                    new Message
                    {
                        role = "user",
                        content = decodedHtmlContent
                    }
                }
            };
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string message = JsonSerializer.Serialize<JsonMessage>(jsonMessage, options);

            StringContent postContent = new StringContent(message, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("v1/chat/completions", postContent);

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error: {response.StatusCode} - {content}");
                throw new Exception($"Error: {response.StatusCode} - {content}");
            }

            CGPTResponse cGPTResponse = JsonSerializer.Deserialize<CGPTResponse>(content, options);

            return new ChatGPTResponse
            {
                Response = cGPTResponse.choices[0].message.content
            
            };
            _logger.LogInformation($"Resume improved: {cGPTResponse.choices[0].message.content}");
        }
    }
}
