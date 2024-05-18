using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("Vote")]
public partial class Vote
{
    [Key]
    public int Id { get; set; }

    public string? VoteValue { get; set; }

    [InverseProperty("Vote")]
    public virtual ICollection<UserVote> UserVotes { get; set; } = new List<UserVote>();
}
