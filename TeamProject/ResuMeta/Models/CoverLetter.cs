using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("CoverLetter")]
public partial class CoverLetter
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }

    [StringLength(100)]
    public string? Title { get; set; }

    [StringLength(100)]
    public string? HiringManager { get; set; }

    [StringLength(500)]
    public string? Body { get; set; }

    [Column("CoverLetter")]
    public string? CoverLetter1 { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("CoverLetters")]
    public virtual UserInfo? UserInfo { get; set; }
}
