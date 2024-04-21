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

    public class ResumeTemplateService : IResumeTemplateService
    {
        private readonly ILogger<CoverLetterService> _logger;
        private readonly IResumeTemplateRepository _resumeTemplateRepo;
        public ResumeTemplateService(
            ILogger<CoverLetterService> logger,
            IResumeTemplateRepository resumeTemplateRepo
            )
        {
            _logger = logger;
            _resumeTemplateRepo = resumeTemplateRepo;
        }

        public List<ResumeVM> GetAllResumeTemplates()
        {
            return _resumeTemplateRepo.GetAllResumeTemplates();
        }

        public ResumeVM GetResumeTemplateHtml(int templateId)
        {
            return _resumeTemplateRepo.GetResumeTemplateHtml(templateId);
        }
    }
}