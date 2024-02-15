using ResuMeta.Models;

namespace ResuMeta.DAL.Abstract
{
    public interface ISkillsRepository : IRepository<Skill>
    {
        IEnumerable<Skill> GetSkillsBySubstring(string skillSubstring);
    }
}
