using ResuMeta.Models.DTO;
using ResuMeta.ViewModels;
using System.Text.Json;

namespace ResuMeta.Services.Abstract
{
    public interface IApplicationTrackerService
    {
        List<ApplicationTrackerVM> GetApplicationsByUserId(int userId);
        void AddApplication(JsonElement content);
        void DeleteApplication(int applicationId);
    }
}