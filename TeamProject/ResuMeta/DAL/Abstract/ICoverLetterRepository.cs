using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Abstract
{
    public interface ICoverLetterRepository : IRepository<CoverLetter>
    {
        CoverLetterVM GetCoverLetter(int coverLetterId);
        CoverLetterVM GetCoverLetterHtml(int coverLetterId);
        List<CoverLetterVM> GetAllCoverLetters(int userId);
    }
}
