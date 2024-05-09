using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface IProfileService
    {
        // int AddProfile(JsonElement content);
        Task<ProfileVM> GetProfile(int profileId);
        bool SaveProfile(int userId, ProfileVM profile);
        Task<List<ProfileVM>> SearchProfile(string keyWord);
    }
}