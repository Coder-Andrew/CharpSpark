using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface ICoverLetterService
    {
        CoverLetterVM AddCoverLetterInfo(JsonElement coverLetterInfo);
        CoverLetterVM GetCoverLetter();
    }
}