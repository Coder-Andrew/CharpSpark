using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("Education")]
public partial class Education
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    public int? ResumeId { get; set; }

    [StringLength(100)]
    public string? Institution { get; set; }

    [StringLength(250)]
    public string? EducationSummary { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool? Completion { get; set; }

    [InverseProperty("Education")]
    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();

    [ForeignKey("ResumeId")]
    [InverseProperty("Educations")]
    public virtual Resume? Resume { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("Educations")]
    public virtual UserInfo? UserInfo { get; set; }
}
