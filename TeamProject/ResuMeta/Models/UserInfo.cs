using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("UserInfo")]
public partial class UserInfo
{
    [Key]
    public int Id { get; set; }

    [Column("ASPNetIdentityId")]
    [StringLength(450)]
    public string? AspnetIdentityId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [StringLength(12)]
    public string? PhoneNumber { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(250)]
    public string? Summary { get; set; }

    [StringLength(2048)]
    public byte[]? ProfilePicturePath { get; set; }

    [InverseProperty("UserInfo")]
    public virtual ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();

    [InverseProperty("UserInfo")]
    public virtual ICollection<ApplicationTracker> ApplicationTrackers { get; set; } = new List<ApplicationTracker>();

    [InverseProperty("UserInfo")]
    public virtual ICollection<Education> Educations { get; set; } = new List<Education>();

    [InverseProperty("UserInfo")]
    public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; } = new List<EmploymentHistory>();

    [InverseProperty("UserInfo")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("UserInfo")]
    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();

    [InverseProperty("UserInfo")]
    public virtual ICollection<UserSkill> UserSkills { get; set; } = new List<UserSkill>();

    [InverseProperty("UserInfo")]
    public virtual ICollection<CoverLetter> CoverLetters { get; set; } = new List<CoverLetter>();

    [InverseProperty("UserInfo")]
    public virtual Profile? Profile {get; set;}

    [InverseProperty("UserInfo")]
    public virtual ICollection<UserVote>? UserVotes { get; set; } = new List<UserVote>();
}
