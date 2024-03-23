using ResuMeta.Models;

namespace ResuMeta.DAL.Abstract
{
    public interface IUserInfoRepository : IRepository<UserInfo>
    {
        UserInfo GetUserInfoByAspNetId(string aspNetId);
    }
}
