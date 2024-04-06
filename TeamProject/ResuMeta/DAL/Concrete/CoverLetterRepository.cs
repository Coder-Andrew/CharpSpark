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
    }
}