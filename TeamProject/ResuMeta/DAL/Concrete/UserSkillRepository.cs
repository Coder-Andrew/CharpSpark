using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;

namespace ResuMeta.DAL.Concrete
{
    public class UserSkillRepository : Repository<UserSkill>, IUserSkillRepository
    {
        DbSet<UserSkill> _userSkills;
        public UserSkillRepository(ResuMetaDbContext context) : base(context)
        {
            _userSkills = context.UserSkills;
        }
        public IEnumerable<UserSkill> GetSkillsByResumeId(int resumeId)
        {
            return _userSkills.Where(x => x.ResumeId == resumeId).ToList();
        }
    }
}
