﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("UserSkill")]
public partial class UserSkill
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    public int? ResumeId { get; set; }

    public int? SkillId { get; set; }

    public int? MonthDuration { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("UserSkills")]
    public virtual Resume? Resume { get; set; }

    [ForeignKey("SkillId")]
    [InverseProperty("UserSkills")]
    public virtual Skill? Skill { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("UserSkills")]
    public virtual UserInfo? UserInfo { get; set; }
}
