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
        public string? Id { get; set; }
        public string? HiringManager { get; set; }
        public string? Body { get; set; }
    }

    public class CoverLetterService : ICoverLetterService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CoverLetterService> _logger;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IRepository<CoverLetter> _coverLetterRepository;
        private readonly ICoverLetterRepository _coverLetterRepo;
        public CoverLetterService(
            ILogger<CoverLetterService> logger,
            UserManager<ApplicationUser> userManager,
            IRepository<UserInfo> userInfo,
            IRepository<CoverLetter> coverLetterRepository,
            ICoverLetterRepository coverLetterRepo
            )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _coverLetterRepository = coverLetterRepository;
            _coverLetterRepo = coverLetterRepo;
        }
        
        public int AddCoverLetterInfo(JsonElement response)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                JsonCoverLetter coverLetterInfo = JsonSerializer.Deserialize<JsonCoverLetter>(response, options)!;
                CoverLetter coverLetter = new CoverLetter
                {
                    UserInfoId = Int32.Parse(coverLetterInfo.Id!),
                    HiringManager = coverLetterInfo.HiringManager,
                    Body = coverLetterInfo.Body
                };

                _coverLetterRepository.AddOrUpdate(coverLetter);

                return coverLetter.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
        }

        public CoverLetterVM GetCoverLetter(int coverLetterId)
        {
            return _coverLetterRepo.GetCoverLetter(coverLetterId);
        }
    }
}