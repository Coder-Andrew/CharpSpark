using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface IProfileService
    {
        int AddProfile(JsonElement content);
        Task<ProfileVM> GetProfile(int profileId);
        void SaveProfileById(JsonElement content);
    }
}