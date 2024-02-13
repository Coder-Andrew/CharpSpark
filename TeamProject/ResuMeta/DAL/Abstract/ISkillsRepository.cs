using ResuMeta.Models;

namespace ResuMeta.DAL.Abstract
{
    public interface ISkillsRepository
    {
        IEnumerable<Skill> GetSkills(string skillSubstring);
    }
}
