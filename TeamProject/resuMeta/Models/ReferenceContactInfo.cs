using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("ReferenceContactInfo")]
public partial class ReferenceContactInfo
{
    [Key]
    public int Id { get; set; }

    public int? EmployementHistoryId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(12)]
    public string? PhoneNumber { get; set; }

    [ForeignKey("EmployementHistoryId")]
    [InverseProperty("ReferenceContactInfos")]
    public virtual EmployementHistory? EmployementHistory { get; set; }
}
