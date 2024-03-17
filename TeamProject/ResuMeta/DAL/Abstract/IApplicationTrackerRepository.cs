using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Abstract
{
    public interface IApplicationTrackerRepository : IRepository<ApplicationTracker>
    {
        List<ApplicationTrackerVM> GetApplicationsByUserId(int userId);
    }
}