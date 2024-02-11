using ResuMeta.Models;
using System.Text.Json;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ResuMeta.Services.Concrete
{
    class JsonDegree
    {
        public string? type { get; set; }
        public string? major { get; set; }
        public string? minor { get; set; }
    }
    class JsonEducation
    {
        public string? institution { get; set; }
        public string? educationSummary { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public string? complete { get; set; }
        public List<JsonDegree>? degree { get; set; }
    }
    class Root
    {
        public string? id { get; set; }
        public List<JsonEducation>? education { get; set; }
    }
    public class ResumeService : IResumeService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IRepository<Education> _education;
        private readonly IRepository<Degree> _degree;
        private readonly ILogger<ResumeService> _logger;
        public ResumeService(ILogger<ResumeService> logger, UserManager<IdentityUser> userManager, IRepository<UserInfo> userInfo, IRepository<Education> education, IRepository<Degree> degree)
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _education = education;
            _degree = degree;
        }

        public void AddResumeInfo(JsonElement response)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                
            };
            try
            {
                Root resumeInfo = JsonSerializer.Deserialize<Root>(response, options)!;
                if (resumeInfo.education == null)
                {
                    throw new Exception("Invalid input");
                }
                foreach (JsonEducation ed in resumeInfo.education!)
                {
                    if (ed.degree == null)
                    {
                        throw new Exception("Invalid input");
                    }
                    Education currEducation = new Education
                    {
                        UserInfoId = Int32.Parse(resumeInfo.id),
                        Institution = ed.institution,
                        EducationSummary = ed.educationSummary,
                        StartDate = DateOnly.Parse(ed.startDate!),
                        EndDate = DateOnly.Parse(ed.endDate!),
                        Completion = bool.Parse(ed.complete!)
                    };
                    Education newUserEd = _education.AddOrUpdate(currEducation);
                    foreach (JsonDegree degree in ed.degree!)
                    {
                        Degree currDegree = new Degree
                        {
                            EducationId = newUserEd.Id,
                            Type = degree.type,
                            Major = degree.major,
                            Minor = degree.minor
                        };
                        _degree.AddOrUpdate(currDegree);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
        }
    }
}