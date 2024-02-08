using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace resuMeta.Models;

[Table("Degree")]
public partial class Degree
{
    [Key]
    public int Id { get; set; }

    public int? EducationId { get; set; }

    [StringLength(100)]
    public string? Type { get; set; }

    [StringLength(50)]
    public string? Major { get; set; }

    [StringLength(50)]
    public string? Minor { get; set; }

    [ForeignKey("EducationId")]
    [InverseProperty("Degrees")]
    public virtual Education? Education { get; set; }
}
