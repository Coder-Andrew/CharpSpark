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
    class JsonResume
    {
        public string? resumeId { get; set; }
        public string? title { get; set; }
        public string? htmlContent { get; set; }
    }
    class JsonProject
    {
        public string? name { get; set; }
        public string? link { get; set; }
        public string? summary { get; set; }
    }
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
    class JsonAchievement
    {
        public string? title { get; set; }
        public string? body { get; set; }
    }
    class Root
    {
        public string? id { get; set; }
        public List<JsonEducation>? education { get; set; }
        public List<JsonSkill>? skills { get; set; }
        public List<JsonResume>? resume { get; set; }
        public List<JsonAchievement>? achievements { get; set; }
        public List<JsonProject>? projects { get; set; }
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
        private readonly IRepository<Achievement> _achievementRepository;
        private readonly IRepository<Project> _projectRepository;
        public ResumeService(
            ILogger<ResumeService> logger,
            UserManager<IdentityUser> userManager,
            IRepository<UserInfo> userInfo,
            IRepository<Education> education,
            IRepository<Degree> degree,
            IRepository<UserSkill> userSkills,
            ISkillsRepository skillsRepository,
            IRepository<Resume> resumeRepository,
            IRepository<Achievement> achievementRepository,
            IRepository<Project> projectRepository
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
            _achievementRepository = achievementRepository;
            _projectRepository = projectRepository;
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
                Resume resume = _resumeRepository.AddOrUpdate(new Resume { UserInfoId = Int32.Parse(resumeInfo.id!) });
                foreach (JsonEducation ed in resumeInfo.education!)
                {
                    if (ed.degree == null)
                    {
                        throw new Exception("Invalid input");
                    }
                    Education currEducation = new Education
                    {
                        UserInfoId = Int32.Parse(resumeInfo.id!),
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
                }
                foreach (JsonSkill jsonSkill in resumeInfo.skills!)
                {
                    Skill skill = _skillsRepository.FindById(jsonSkill.skillId);
                    if (skill != null)
                    {
                        _userSkills.AddOrUpdate(new UserSkill
                        {
                            UserInfoId = Int32.Parse(resumeInfo.id!),
                            SkillId = skill.Id,
                            Resume = resume
                        });
                    }
                }
                foreach (JsonAchievement jsonAch in resumeInfo.achievements!)
                {
                    _achievementRepository.AddOrUpdate(new Achievement
                    {
                        // IMPORTANT: there will be an error if a user enters a string which is longer than the model's
                        // string length restriction. Potential vulnerability down the line
                        UserInfoId = Int32.Parse(resumeInfo.id!),
                        Achievement1 = jsonAch.title,
                        Summary = jsonAch.body,
                        Resume = resume
                    });
                }
                foreach (JsonProject jsonProj in resumeInfo.projects!)
                {
                    _projectRepository.AddOrUpdate(new Project
                    {
                        UserInfoId = Int32.Parse(resumeInfo.id!),
                        Name = jsonProj.name,
                        Link = jsonProj.link,
                        Summary = jsonProj.summary,
                        Resume = resume
                    });
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
                .Select(s => new SkillDTO
                {
                    Id = s.Id,
                    SkillName = s.SkillName
                }).ToList();
        }

        public ResumeVM GetResume(int resumeId, string userEmail)
        {
            Resume userResume = _resumeRepository.FindById(resumeId);
            if (userResume == null)
            {
                throw new Exception("Resume not found");
            }

            try
            {
                var education = userResume.Educations.Select(e => e.Degrees).ToList();
                var degrees = new List<DegreeVM>();
                foreach (var ed in userResume.Educations)
                {
                    foreach (var d in ed.Degrees)
                    {
                        degrees.Add(new DegreeVM
                        {
                            Type = d.Type,
                            Major = d.Major,
                            Minor = d.Minor
                        });
                    }
                }
                return new ResumeVM
                {
                    Degree = degrees,
                    Education = userResume.Educations.Select(e => new EducationVM
                    {
                        EducationSummary = e.EducationSummary,
                        Institution = e.Institution,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Completion = e.Completion
                    }).ToList(),
                    ResumeId = resumeId,
                    FirstName = userResume.UserInfo?.FirstName,
                    LastName = userResume.UserInfo?.LastName,
                    Email = userEmail,
                    Phone = userResume.UserInfo?.PhoneNumber,
                    Skills = userResume.UserSkills.Select(s => new SkillVM
                    {
                        SkillName = s.Skill?.SkillName
                    }).ToList(),
                    Achievements = userResume.Achievements.Select(a => new AchievementVM
                    {
                        title = a.Achievement1,
                        summary = a.Summary
                    }).ToList(),
                    Projects = userResume.Projects.Select(p => new ProjectVM
                    {
                        Name = p.Name,
                        Link = p.Link,
                        Summary = p.Summary
                    }).ToList()
                };
            }
            catch
            {
                throw new Exception("Error getting resume");
            }
        }

        public void SaveResumeById(JsonElement content)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            try
            {
                JsonResume resumeContent = JsonSerializer.Deserialize<JsonResume>(content, options)!;
                if (resumeContent.htmlContent == null)
                {
                    throw new Exception("Invalid input");
                }
                Resume resume = _resumeRepository.FindById(Int32.Parse(resumeContent.resumeId!));
                if (resume == null)
                {
                    throw new Exception("Resume not found");
                }
                resume.Resume1 = resumeContent.htmlContent;
                resume.Title = resumeContent.title;
                _resumeRepository.AddOrUpdate(resume);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error deserializing json");
                throw new Exception("Error deserializing json");
            }
        }

        public ResumeVM GetResumeHtml(int resumeId)
        {
            Resume resume = _resumeRepository.FindById(resumeId);
            if (resume == null)
            {
                throw new Exception("Resume not found");
            }
            ResumeVM resumeVM = new ResumeVM
            {
                ResumeId = resumeId,
                Title = resume.Title,
                HtmlContent = resume.Resume1
            };
            return resumeVM;
        }

        public List<KeyValuePair<int, string>> GetResumeIdList(int userId)
        {
            var resumeIdList = _resumeRepository.GetAll()
            .Where(x => x.UserInfoId == userId && x.Resume1 != null)
            .Select(x => new KeyValuePair<int, string>(x.Id, x.Title!))
            .ToList();
            
            return resumeIdList;
        }
    }
}