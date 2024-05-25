using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("Profile")]
public partial class Profile
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    [Column("Resume")]
    public string? ResumeHtml { get; set; }

    public int? ResumeId { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public int? ProfileScore { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("Profile")]
    public virtual UserInfo? UserInfo { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("Profile")]
    public virtual Resume? Resume { get; set; }

    [InverseProperty("Profile")]
    public virtual ICollection<Follower> Followers { get; set; } = new List<Follower>();

    [InverseProperty("FollowerProfile")]
    public virtual ICollection<Follower> Following { get; set; } = new List<Follower>();

    [InverseProperty("Profile")]
    public virtual ProfileViews ProfileViews { get; set; } = new ProfileViews();
}
