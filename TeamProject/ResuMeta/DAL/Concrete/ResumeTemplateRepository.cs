using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Concrete
{
    public class ResumeTemplateRepository : Repository<ResumeTemplate>, IResumeTemplateRepository
    {
        private readonly DbSet<ResumeTemplate> _resumeTemplates;
        public ResumeTemplateRepository(ResuMetaDbContext context) : base(context)
        {
            _resumeTemplates = context.ResumeTemplates;
        }

        public ResumeVM GetResumeTemplateHtml(int templateId)
        {
            ResumeTemplate? resumeTemplate = _resumeTemplates.FirstOrDefault(r => r.Id == templateId);
            if(resumeTemplate == null)
            {
                throw new Exception("Resume Template not found");
            }

            ResumeVM resumeVM = new ResumeVM
            {
                ResumeId = resumeTemplate.Id,
                Title = resumeTemplate.Title,
                HtmlContent = resumeTemplate.Template1
            };
            return resumeVM;
        }

        public List<ResumeVM> GetAllResumeTemplates()
        {
            var resumeTemplateList = _resumeTemplates
                .Where(x => x.Template1 != null)
                .Select(x => new ResumeVM
                {
                    ResumeId = x.Id,
                    Title = x.Title,
                    HtmlContent = x.Template1
                })
                .ToList();

            return resumeTemplateList;
        }
    }
}