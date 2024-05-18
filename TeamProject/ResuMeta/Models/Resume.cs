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

    [StringLength(100)]
    public string? Title { get; set; }

    [Column("Resume")]
    public string? Resume1 { get; set; }

    [InverseProperty("Resume")]
    public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();

    [InverseProperty("Resume")]
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    [InverseProperty("Resume")]
    public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; } = new List<EmploymentHistory>();

    [InverseProperty("Resume")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [ForeignKey("UserInfoId")]
    [InverseProperty("Resumes")]
    public virtual UserInfo? UserInfo { get; set; }

    [InverseProperty("Resume")]
    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();

    [InverseProperty("Resume")]
    public virtual Profile? Profile { get; set; }

    [InverseProperty("Resume")]
    public virtual ICollection<UserVote> UserVotes { get; set; } = new List<UserVote>();
}
