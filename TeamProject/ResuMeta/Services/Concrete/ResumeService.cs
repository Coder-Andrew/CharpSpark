using ResuMeta.Models;
using System.Text.Json;
using ResuMeta.DAL.Abstract;
using ResuMeta.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;

namespace ResuMeta.Services.Concrete
{
    class JsonSkill
    {
        public int skillId { get; set; }
    }
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
        public List<JsonSkill>? skills { get; set; }
    }
    public class ResumeService : IResumeService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ResumeService> _logger;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IRepository<Education> _education;
        private readonly IRepository<Degree> _degree;
        private readonly IRepository<UserSkill> _userSkills;
        private readonly ISkillsRepository _skillsRepository;
        private readonly IRepository<Resume> _resumeRepository;
        public ResumeService(
            ILogger<ResumeService> logger,
            UserManager<IdentityUser> userManager,
            IRepository<UserInfo> userInfo,
            IRepository<Education> education,
            IRepository<Degree> degree,
            IRepository<UserSkill> userSkills,
            ISkillsRepository skillsRepository,
            IRepository<Resume> resumeRepository
            )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _education = education;
            _degree = degree;
            _userSkills = userSkills;
            _skillsRepository = skillsRepository;
            _resumeRepository = resumeRepository;
        }

        public int AddResumeInfo(JsonElement response)
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
                Resume resume = _resumeRepository.AddOrUpdate(new Resume { UserInfoId = Int32.Parse(resumeInfo.id) });
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
                        Completion = bool.Parse(ed.complete!),
                        Resume = resume,
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
                    foreach(JsonSkill jsonSkill in resumeInfo.skills!)
                    {
                        Skill skill = _skillsRepository.FindById(jsonSkill.skillId);
                        if (skill != null)
                        {
                            _userSkills.AddOrUpdate(new UserSkill
                            {
                                UserInfoId = Int32.Parse(resumeInfo.id),
                                SkillId = skill.Id,
                                Resume = resume
                            });
                        }
                    }
                }
                return resume.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
        }

        public IEnumerable<SkillDTO> GetSkillsBySubstring(string skillsSubstring)
        {
            return _skillsRepository.GetSkillsBySubstring(skillsSubstring)
                .Select(s => new SkillDTO { 
                    Id = s.Id,
                    SkillName = s.SkillName                
                }).ToList();
        }

        public ResumeVM GetResume(int resumeId)
        {
            Resume userResume = _resumeRepository.FindById(resumeId);
            if (userResume == null)
            {
                throw new Exception("Resume not found");
            }

            try
            {
                return new ResumeVM
                {
                    Degree = userResume.Educations.FirstOrDefault()?.Degrees.Select(d => new DegreeVM
                    {
                        Type = d.Type,
                        Major = d.Major,
                        Minor = d.Minor
                    }).FirstOrDefault(),
                    Education = new EducationVM
                    {
                        EducationSummary = userResume.Educations.FirstOrDefault()?.EducationSummary,
                        Institution = userResume.Educations.FirstOrDefault()?.Institution,
                        StartDate = userResume.Educations.FirstOrDefault()?.StartDate,
                        EndDate = userResume.Educations.FirstOrDefault()?.EndDate,
                        Completion = userResume.Educations.FirstOrDefault()?.Completion
                    },
                    ResumeId = resumeId,
                    Skills = userResume.UserSkills.Select(s => new SkillVM
                    {
                        SkillName = s.Skill.SkillName
                    }).ToList()
                };
            }
            catch
            {
                throw new Exception("Error getting resume");
            }
        }

    }
}