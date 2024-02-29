using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Concrete
{
    public class ResumeRepository : Repository<ResumeVM>, IResumeRepository
    {
        private readonly DbSet<Resume> _resumes;
        public ResumeRepository(ResuMetaDbContext context) : base(context)
        {
            _resumes = context.Resumes;
        }
        public List<ResumeVM> GetAllResumes(int userId)
        {
            var resumeList = _resumes
            .Where(x => x.UserInfoId == userId && x.Resume1 != null)
            .Select(x => new ResumeVM
            {
                ResumeId = x.Id,
                Title = x.Title,
                HtmlContent = x.Resume1
            })
            .ToList();
            return resumeList;
        }

        public List<KeyValuePair<int, string>> GetResumeIdList(int userId)
         {
            var resumeIdList = _resumes
            .Where(x => x.UserInfoId == userId && x.Resume1 != null)
            .Select(x => new KeyValuePair<int, string>(x.Id, x.Title!))
            .ToList();
            
            return resumeIdList;
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

        public ResumeVM GetResumeHtml(int resumeId)
        {
            Resume? resume = _resumes.FirstOrDefault(r => r.Id == resumeId);
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
    }
}
