using Microsoft.EntityFrameworkCore;
using ResuMeta.DAL.Abstract;
using ResuMeta.Models;

namespace ResuMeta.DAL.Concrete
{
    public class SkillsRepository : Repository<Skill>, ISkillsRepository
    {
        private readonly DbSet<Skill> _skills;
        public SkillsRepository(ResuMetaDbContext context) : base(context)
        {
            _skills = context.Skills;
        }
        public IEnumerable<Skill> GetSkills(string skillSubstring)
        {
            return _skills.Where(s => s.Skill1.Contains(skillSubstring)).ToList();
        }
    }
}
