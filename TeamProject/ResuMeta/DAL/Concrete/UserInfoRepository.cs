using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;

namespace ResuMeta.DAL.Concrete
{
    public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
    {
        DbSet<UserInfo> _userInfos;
        public UserInfoRepository(ResuMetaDbContext context) : base(context)
        {
            _userInfos = context.UserInfos;
        }

        public UserInfo GetUserInfoByAspNetId(string aspNetId)
        {
            return _userInfos.Where(x => x.AspnetIdentityId == aspNetId).FirstOrDefault();
        }
    }
}
