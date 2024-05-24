using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("Follower")]
public partial class Follower
{
    [Key]
    public int Id { get; set; }

    public int? ProfileId { get; set; }
    public int? FollowerId { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime Timestamp { get; set; }

    [ForeignKey("ProfileId")]
    [InverseProperty("Followers")]
    public virtual Profile? Profile { get; set; }

    [ForeignKey("FollowerId")]
    [InverseProperty("Following")]
    public virtual Profile? FollowerProfile { get; set; }    
}
