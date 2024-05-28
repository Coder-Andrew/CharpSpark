using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("ProfileViews")]
public partial class ProfileViews
{
    [Key]
    public int Id { get; set; }

    public int? ProfileId { get; set; }
    public int? ViewCount { get; set; }

    [ForeignKey("ProfileId")]
    [InverseProperty("ProfileViews")]
    public virtual Profile? Profile { get; set; }
}
