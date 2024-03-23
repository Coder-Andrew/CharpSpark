using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

public partial class Project
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    public int? ResumeId { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(250)]
    public string? Link { get; set; }

    [StringLength(250)]
    public string? Summary { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("Projects")]
    public virtual Resume? Resume { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("Projects")]
    public virtual UserInfo? UserInfo { get; set; }
}
