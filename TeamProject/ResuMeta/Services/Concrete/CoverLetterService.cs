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
    class JsonCoverLetterContent
    {
        public string? coverLetterId { get; set; }
        public string? title { get; set; }
        public string? htmlContent { get; set; }
    }
    class JsonCoverLetter
    {
        public string? Id { get; set; }
        public string? HiringManager { get; set; }
        public string? Body { get; set; }
    }
    class JsonCoverLetterComplete
    {
        public string? Id { get; set; }
        public string? HiringManager { get; set; }
        public string? Body { get; set; }
        public string? coverLetterId { get; set; }
        public string? title { get; set; }
        public string? htmlContent { get; set; }
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

        public void SaveCoverLetterById(JsonElement content)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                JsonCoverLetterContent coverLetterContent = JsonSerializer.Deserialize<JsonCoverLetterContent>(content, options)!;
                if (coverLetterContent.htmlContent == null)
                {
                    throw new Exception("Invalid input");
                }

                CoverLetter coverLetter = _coverLetterRepository.FindById(Int32.Parse(coverLetterContent.coverLetterId!));
                coverLetter.CoverLetter1 = coverLetterContent.htmlContent;
                coverLetter.Title = coverLetterContent.title;
                _coverLetterRepository.AddOrUpdate(coverLetter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
        }

        public CoverLetterVM GetCoverLetterHtml(int coverLetterId)
        {
            return _coverLetterRepo.GetCoverLetterHtml(coverLetterId);
        }

        public List<CoverLetterVM> GetAllCoverLetters(int userId)
        {
            return _coverLetterRepo.GetAllCoverLetters(userId);
        }

        public void DeleteCoverLetter(int coverLetterId)
        {
            CoverLetter coverLetter = _coverLetterRepository.FindById(coverLetterId);
            _coverLetterRepository.Delete(coverLetter);
        }


        public int TailoredCoverLetter(JsonElement response)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                JsonCoverLetterComplete coverLetterComplete = JsonSerializer.Deserialize<JsonCoverLetterComplete>(response, options)!;

                // Add cover letter info
                CoverLetter coverLetter = new CoverLetter
                {
                    UserInfoId = Int32.Parse(coverLetterComplete.Id!),
                    HiringManager = coverLetterComplete.HiringManager,
                    Body = coverLetterComplete.Body
                };
                _coverLetterRepository.AddOrUpdate(coverLetter);

                // Save cover letter content
                if (coverLetterComplete.htmlContent == null)
                {
                    throw new Exception("Invalid input");
                }
                coverLetter.CoverLetter1 = coverLetterComplete.htmlContent;
                coverLetter.Title = coverLetterComplete.title;
                _coverLetterRepository.AddOrUpdate(coverLetter);

                return coverLetter.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
        }
    }
}