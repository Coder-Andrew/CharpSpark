using ResuMeta.Models;
using ResuMeta.ViewModels;

namespace ResuMeta.DAL.Abstract
{
    public interface IProfileRepository : IRepository<ProfileVM>
    {
        ProfileVM GetProfileById(int userId, string userName, string firstName, string lastName, byte[] profilePicturePath);
        ProfileVM2 GetProfileById(int userId);
    }
}
