using ResuMeta.Models.DTO;
using ResuMeta.Models;
using System.Text.Json;
using ResuMeta.ViewModels;

namespace ResuMeta.Services.Abstract
{
    public interface ICoverLetterService
    {
        int AddCoverLetterInfo(JsonElement coverLetterInfo);
        CoverLetterVM GetCoverLetter(int coverLetterId);
        void SaveCoverLetterById(JsonElement content);
        CoverLetterVM GetCoverLetterHtml(int coverLetterId);
        List<CoverLetterVM> GetAllCoverLetters(int userId);
        void DeleteCoverLetter(int coverLetterId);
        int TailoredCoverLetter(JsonElement response);
    }
}