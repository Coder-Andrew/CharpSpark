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
        public IEnumerable<Skill> GetSkillsBySubstring(string skillSubstring)
        {
            return _skills.Where(s => s.SkillName.Contains(skillSubstring)).ToList();
        }
    }
}
