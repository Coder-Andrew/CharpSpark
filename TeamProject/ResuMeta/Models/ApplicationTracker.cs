using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("ApplicationTracker")]
public partial class ApplicationTracker
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    [StringLength(100)]
    public string? JobTitle { get; set; }

    [StringLength(100)]
    public string? CompanyName { get; set; }

    [Column("JobListingURL")]
    [StringLength(250)]
    public string? JobListingUrl { get; set; }

    public DateOnly? AppliedDate { get; set; }

    public DateOnly? ApplicationDeadline { get; set; }

    [StringLength(100)]
    public string? Status { get; set; }

    [StringLength(250)]
    public string? Notes { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("ApplicationTrackers")]
    public virtual UserInfo? UserInfo { get; set; }
}
