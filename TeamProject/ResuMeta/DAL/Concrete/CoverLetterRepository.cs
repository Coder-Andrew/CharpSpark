using System.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Concrete
{
    public class CoverLetterRepository : Repository<CoverLetter>, ICoverLetterRepository
    {
        private readonly DbSet<CoverLetter> _coverLetters;
        public CoverLetterRepository(ResuMetaDbContext context) : base(context)
        {
            _coverLetters = context.CoverLetters;
        }

        public CoverLetterVM GetCoverLetter(int coverLetterId)
        {
            CoverLetter? coverLetter = _coverLetters.FirstOrDefault(r => r.Id == coverLetterId);
            if(coverLetter == null)
            {
                throw new Exception("Cover Letter not found");
            }

            try
            {
                CoverLetterVM coverLetterVM = new CoverLetterVM
                {
                    CoverLetterId = coverLetter.Id,
                    FirstName = coverLetter.UserInfo?.FirstName,
                    LastName = coverLetter.UserInfo?.LastName,
                    Title = coverLetter.Title,
                    HiringManager = coverLetter.HiringManager,
                    Body = coverLetter.Body
                };

                return coverLetterVM;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting cover letter", ex);
            }
        }

        public CoverLetterVM GetCoverLetterHtml(int coverLetterId)
        {
            CoverLetter? coverLetter = _coverLetters.FirstOrDefault(r => r.Id == coverLetterId);
            if(coverLetter == null)
            {
                throw new Exception("Cover Letter not found");
            }

            CoverLetterVM coverLetterVM = new CoverLetterVM
            {
                CoverLetterId = coverLetter.Id,
                Title = coverLetter.Title,
                HtmlContent = coverLetter.CoverLetter1
            };
            return coverLetterVM;
        }

        public List<CoverLetterVM> GetAllCoverLetters(int userId)
        {
            var coverLetterList = _coverLetters
            .Where(x => x.UserInfoId == userId && x.CoverLetter1 != null)
            .Select(x => new CoverLetterVM
            {
                CoverLetterId = x.Id,
                Title = x.Title,
                HtmlContent = x.CoverLetter1
            })
            .ToList();
            return coverLetterList;
        }
    }
}