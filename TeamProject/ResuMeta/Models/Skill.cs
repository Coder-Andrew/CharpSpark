using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Index("SkillName", Name = "UQ__Skills__B63C6571EA406E9A", IsUnique = true)]
public partial class Skill
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string SkillName { get; set; } = null!;

    [InverseProperty("Skill")]
    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}
