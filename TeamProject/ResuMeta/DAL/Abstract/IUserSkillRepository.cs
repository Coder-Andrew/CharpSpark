using ResuMeta.Models;

namespace ResuMeta.DAL.Abstract
{
    public interface IUserSkillRepository : IRepository<UserSkill>
    {
        IEnumerable<UserSkill> GetSkillsByResumeId(int resumeId);
    }
}
