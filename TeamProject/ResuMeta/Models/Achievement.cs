using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

public partial class Achievement
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    public int? ResumeId { get; set; }

    [Column("Achievement")]
    [StringLength(100)]
    public string? Achievement1 { get; set; }

    [StringLength(250)]
    public string? Summary { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("Achievements")]
    public virtual Resume? Resume { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("Achievements")]
    public virtual UserInfo? UserInfo { get; set; }
}
