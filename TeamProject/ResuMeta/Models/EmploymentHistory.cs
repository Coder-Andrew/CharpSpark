using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("EmploymentHistory")]
public partial class EmploymentHistory
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    public int? ResumeId { get; set; }

    [StringLength(100)]
    public string? Company { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? Location { get; set; }

    [StringLength(100)]
    public string? JobTitle { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [InverseProperty("EmploymentHistory")]
    public virtual ICollection<ReferenceContactInfo> ReferenceContactInfos { get; set; } = new List<ReferenceContactInfo>();

    [ForeignKey("ResumeId")]
    [InverseProperty("EmploymentHistories")]
    public virtual Resume? Resume { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("EmploymentHistories")]
    public virtual UserInfo? UserInfo { get; set; }
}
