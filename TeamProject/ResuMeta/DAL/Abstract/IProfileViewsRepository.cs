using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Abstract
{
    public interface IProfileViewsRepository : IRepository<ProfileViews>
    {
        int? GetViewCount(int? profileId);
        Task UpdateViewCount(int? profileId);
    }
}
