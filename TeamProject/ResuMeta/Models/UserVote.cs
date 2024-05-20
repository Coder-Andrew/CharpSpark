using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

[Table("UserVote")]
public partial class UserVote
{
    [Key]
    public int Id { get; set; }

    public int? UserInfoId { get; set; }
    public int? ResumeId { get; set; }
    public int? VoteId { get; set; }

    [ForeignKey("UserInfoId")]
    [InverseProperty("UserVotes")]
    public virtual UserInfo? UserInfo { get; set; }

    [ForeignKey("ResumeId")]
    [InverseProperty("UserVotes")]
    public virtual Resume? Resume { get; set; }

    [ForeignKey("VoteId")]
    [InverseProperty("UserVotes")]
    public virtual Vote? Vote { get; set; }

}
