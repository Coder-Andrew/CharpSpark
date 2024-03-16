using ResuMeta.Models;
using System.Text.Json;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using ResuMeta.Data;

namespace ResuMeta.Services.Concrete
{
    class JsonCoverLetter
    {
        public int? UserInfoId { get; set; }
        public int? CoverLetterId { get; set; }
        public string? HiringManager { get; set; }
        public string? Body { get; set; }
    }

    public class CoverLetterService : ICoverLetterService
    {
         private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CoverLetterService> _logger;
        private readonly IRepository<UserInfo> _userInfo;
        private CoverLetterVM coverLetter;
        public CoverLetterService(
            ILogger<CoverLetterService> logger,
            UserManager<ApplicationUser> userManager,
            IRepository<UserInfo> userInfo
            )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
        }
        
        public CoverLetterVM AddCoverLetterInfo(JsonElement response)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                JsonCoverLetter coverLetterInfo = JsonSerializer.Deserialize<JsonCoverLetter>(response, options)!;
                this.coverLetter = new CoverLetterVM
                {
                    UserInfoId = coverLetterInfo.UserInfoId,
                    CoverLetterId = coverLetterInfo.CoverLetterId,
                    HiringManager = coverLetterInfo.HiringManager,
                    Body = coverLetterInfo.Body
                };

                return coverLetter;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
        }

        public CoverLetterVM GetCoverLetter()
        {
            return this.coverLetter;
        }
    }
}