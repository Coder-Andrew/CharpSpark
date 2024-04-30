using Castle.Core.Logging;
using ResuMeta.Models;
using ResuMeta.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ResuMeta.ViewModels;


namespace ResuMeta_Tests
{
    class ChatGPTService_Tests
    {
        private Mock<HttpMessageHandler> handlerMock;
        private HttpClient httpClient;
        private ChatGPTService chatGPTService;
        private Mock<ILogger<ChatGPTService>> logger;

        [SetUp]
        public void Setup()
        {
            // Mock the HttpMessageHandler
            handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()

                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("Here is your resume"),
                })
                .Verifiable();

            httpClient = new HttpClient(handlerMock.Object);


            chatGPTService = new ChatGPTService(httpClient);
            
        }

        // I don't know how to mock ILogger and there are limited numbers of ways to test the chatgpt service
        // I could maybe also test the saving functionality, but that was more of an optional task than required
        // it would involve moqing the repo. There is also a lot of placeholder classes/code since the code doesn't
        // exist

        [Test]
        public void ChatGPTService_GenerateResume_ThrowsException_WithInvalidInput()
        {
            // Arrange
            ResumeVM resume = new ResumeVM
            {
                UserInfoId = 0,
                Resume1 = ""
            };

            // Act & Asser
            Assert.Throws<InvalidDataException>(() => { chatGPTService.GenerateResume(resume); });
        }

        [Test]
        public void ChatGPTService_GenerateResume_ReturnsImprovedResume_WithValidInput()
        {
            // Arrange
            ResumeVM resume = new ResumeVM
            {
                UserInfoId = 0,
                Resume1 = "Here is my resume"
            };

            // Act
            ResumeVM improvedResume = chatGPTService.GenerateResume(resume);
        }
}
