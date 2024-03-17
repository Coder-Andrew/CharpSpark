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

    public int? EmploymentHistoryId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(12)]
    public string? PhoneNumber { get; set; }

    [ForeignKey("EmploymentHistoryId")]
    [InverseProperty("ReferenceContactInfos")]
    public virtual EmploymentHistory? EmploymentHistory { get; set; }
}
