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
    }
}