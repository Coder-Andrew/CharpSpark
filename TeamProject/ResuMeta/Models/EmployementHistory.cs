using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("EmployementHistory")]
public partial class EmployementHistory
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    [StringLength(100)]
    public string? Company { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [InverseProperty("EmployementHistory")]
    public virtual ICollection<ReferenceContactInfo> ReferenceContactInfos { get; set; } = new List<ReferenceContactInfo>();

    [ForeignKey("ResumeId")]
    [InverseProperty("EmployementHistories")]
    public virtual Resume? Resume { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("EmployementHistories")]
    public virtual UserInfo? UserInfo { get; set; }
}
