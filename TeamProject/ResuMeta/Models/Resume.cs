using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("Resume")]
public partial class Resume
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    public int? EducationId { get; set; }

    public int? UserSkillId { get; set; }

    [Column("Resume")]
    [MaxLength(1)]
    public byte[]? Resume1 { get; set; }

    [ForeignKey("EducationId")]
    [InverseProperty("Resumes")]
    public virtual Education? Education { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("Resumes")]
    public virtual UserInfo? UserInfo { get; set; }

    [ForeignKey("UserSkillId")]
    [InverseProperty("Resumes")]
    public virtual UserSkill? UserSkill { get; set; }
}
