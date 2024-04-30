using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("ResumeTemplate")]
public partial class ResumeTemplate
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string? Title { get; set; }

    [Column("Template")]
    public string? Template1 { get; set; }
}
