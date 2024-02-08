using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

public partial class Skill
{
    [Key]
    public int Id { get; set; }

    [Column("Skill")]
    [StringLength(100)]
    public string? Skill1 { get; set; }

    [InverseProperty("Skill")]
    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();
}
