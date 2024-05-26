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
using ResuMeta.DAL.Concrete;

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
    class JsonReferenceContactInfo
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? phoneNumber { get; set; }
    }
    class JsonEmploymentHistory
    {
        public string? company { get; set; }
        public string? description { get; set; }
        public string? location { get; set; }
        public string? jobTitle { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public List<JsonReferenceContactInfo>? referenceContactInfo { get; set; }
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
        public List<JsonEmploymentHistory>? employment { get; set; }
        public List<JsonSkill>? skills { get; set; }
        public List<JsonResume>? resume { get; set; }
        public List<JsonAchievement>? achievements { get; set; }
        public List<JsonProject>? projects { get; set; }
        public string? personalSummary { get; set; }
    }

    public class ResumeService : IResumeService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ResumeService> _logger;
        private readonly IRepository<UserInfo> _userInfo;
        private readonly IRepository<Education> _education;
        private readonly IRepository<Degree> _degree;
        private readonly IRepository<EmploymentHistory> _employmentHistory;
        private readonly IRepository<ReferenceContactInfo> _referenceContactInfo;
        private readonly IRepository<UserSkill> _userSkills;
        private readonly ISkillsRepository _skillsRepository;
        private readonly IRepository<Resume> _resumeRepository;
        private readonly IResumeRepository _resumeRepo;
        private readonly IRepository<Achievement> _achievementRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IUserSkillRepository _userSkillRepository;
        public ResumeService(
            ILogger<ResumeService> logger,
            UserManager<ApplicationUser> userManager,
            IRepository<UserInfo> userInfo,
            IRepository<Education> education,
            IRepository<Degree> degree,
            IRepository<EmploymentHistory> employmentHistory,
            IRepository<ReferenceContactInfo> referenceContactInfo,
            IRepository<UserSkill> userSkills,
            ISkillsRepository skillsRepository,
            IRepository<Resume> resumeRepository,
            IResumeRepository resumeRepo,
            IRepository<Achievement> achievementRepository,
            IRepository<Project> projectRepository,
            IUserSkillRepository userSkillRepository
            )
        {
            _logger = logger;
            _userManager = userManager;
            _userInfo = userInfo;
            _education = education;
            _degree = degree;
            _employmentHistory = employmentHistory;
            _referenceContactInfo = referenceContactInfo;
            _userSkills = userSkills;
            _skillsRepository = skillsRepository;
            _resumeRepository = resumeRepository;
            _resumeRepo = resumeRepo;
            _achievementRepository = achievementRepository;
            _projectRepository = projectRepository;
            _userSkillRepository = userSkillRepository;
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

            // if(resumeInfo.EmploymentHistory != null)
            // {
                foreach (JsonEmploymentHistory eh in resumeInfo.employment!)
                {
                    if (eh.referenceContactInfo == null)
                    {
                        throw new Exception("Invalid input");
                    }
                    EmploymentHistory currEmployment = new EmploymentHistory
                    {
                        UserInfoId = Int32.Parse(resumeInfo.id!),
                        Company = eh.company,
                        Description = eh.description,
                        Location = eh.location,
                        JobTitle = eh.jobTitle,
                        StartDate = DateOnly.Parse(eh.startDate!),
                        EndDate = DateOnly.Parse(eh.endDate!),
                        Resume = resume
                    };
                    EmploymentHistory newUserEmployment = _employmentHistory.AddOrUpdate(currEmployment);
                    _logger.LogInformation("Added or updated EmploymentHistory");
                    foreach (JsonReferenceContactInfo referenceContactInfo in eh.referenceContactInfo!)
                    {
                        ReferenceContactInfo currReference = new ReferenceContactInfo
                        {
                            EmploymentHistoryId = newUserEmployment.Id,
                            FirstName = referenceContactInfo.firstName,
                            LastName = referenceContactInfo.lastName,
                            PhoneNumber = referenceContactInfo.phoneNumber
                        };
                        ReferenceContactInfo newReferenceContactInfo = _referenceContactInfo.AddOrUpdate(currReference);
                        _logger.LogInformation("Added or updated ReferenceContactInfo with ID" );
                    }
                }
           // }
            

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

                UserInfo userInfo = _userInfo.FindById(Int32.Parse(resumeInfo.id!));

                
                if (!string.IsNullOrWhiteSpace(resumeInfo.personalSummary!.ToString())) {
                    userInfo.Summary = resumeInfo.personalSummary.ToString();
                    _userInfo.AddOrUpdate(userInfo);
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
            return _resumeRepo.GetResume(resumeId, userEmail);
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
            return _resumeRepo.GetResumeHtml(resumeId);
        }

        // public List<KeyValuePair<int, string>> GetResumeIdList(int userId)
        // {
        //     return _resumeRepo.GetResumeIdList(userId);
        // }

        public List<ResumeVM> GetAllResumes(int userId)
        {
            return _resumeRepo.GetAllResumes(userId);
        }

        public List<EducationVM> GetEducationByUserInfoId(int userInfoId)
        {
            var educations = _education.GetAll(x => x.UserInfoId == userInfoId)
            .GroupBy(x => new { x.Institution, x.EducationSummary, x.StartDate, x.EndDate, x.Completion })
            .Select(g => g.First())
            .ToList();
            var educationList = new List<EducationVM>();
            foreach(var ed in educations)
            {
                var degree = _degree.GetAll(x => x.EducationId == ed.Id).FirstOrDefault();
                var degreeVM = new DegreeVM
                {
                    Type = degree!.Type,
                    Major = degree.Major,
                    Minor = degree.Minor
                };
                educationList.Add(new EducationVM
                {
                    Institution = ed.Institution,
                    EducationSummary = ed.EducationSummary,
                    StartDate = ed.StartDate,
                    EndDate = ed.EndDate,
                    Completion = ed.Completion,
                    Degree = degreeVM
                });
            }
            return educationList;
        }

        public List<EmploymentHistoryVM> GetEmploymentByUserInfoId(int userInfoId)
        {
            var employments = _employmentHistory.GetAll(x => x.UserInfoId == userInfoId)
            .GroupBy(x => new { x.Company, x.Description, x.Location, x.JobTitle, x.StartDate, x.EndDate })
            .Select(g => g.First())
            .ToList();
            var employmentList = new List<EmploymentHistoryVM>();
            foreach(var eh in employments)
            {
                var referenceContactInfo = _referenceContactInfo.GetAll(x => x.EmploymentHistoryId == eh.Id).FirstOrDefault();
                var referenceVM = new ReferenceContactInfoVM
                {
                    FirstName = referenceContactInfo!.FirstName,
                    LastName = referenceContactInfo.LastName,
                    PhoneNumber = referenceContactInfo.PhoneNumber
                };
                employmentList.Add(new EmploymentHistoryVM
                {
                    Company = eh.Company,
                    Description = eh.Description,
                    Location = eh.Location,
                    JobTitle = eh.JobTitle,
                    StartDate = eh.StartDate,
                    EndDate = eh.EndDate,
                    ReferenceContactInfo = referenceVM
                });
            }
            return employmentList;
        }

        public List<AchievementVM> GetAchievementsByUserInfoId(int userInfoId){
            var achievements = _achievementRepository.GetAll(x => x.UserInfoId == userInfoId)
            .GroupBy(x => new { x.Achievement1, x.Summary })
            .Select(g => g.First())
            .ToList();
            var achievementList = new List<AchievementVM>();
            foreach(var ach in achievements)
            {
                achievementList.Add(new AchievementVM
                {
                    title = ach.Achievement1,
                    summary = ach.Summary
                });
            }
            return achievementList;
        }

        public List<ProjectVM> GetProjectsByUserInfoId(int userInfoId){
            var projects = _projectRepository.GetAll(x => x.UserInfoId == userInfoId)
            .GroupBy(x => new { x.Name, x.Link, x.Summary })
            .Select(g => g.First())
            .ToList();
            var projectList = new List<ProjectVM>();
            foreach(var proj in projects)
            {
                projectList.Add(new ProjectVM
                {
                    Name = proj.Name,
                    Link = proj.Link,
                    Summary = proj.Summary
                });
            }
            return projectList;
        }

        public void DeleteResume(int resumeId)
        {
            
            var resume = _resumeRepository.FindById(resumeId);
    
             var skills = _userSkillRepository.GetSkillsByResumeId(resumeId);
             foreach (var skill in skills)
             {
                 _userSkillRepository.Delete(skill);
             }
            
            _resumeRepository.Delete(resume);
        }
    }
}