using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ResuMeta.Models;

public partial class ResuMetaDbContext : DbContext
{
    public ResuMetaDbContext()
    {
    }

    public ResuMetaDbContext(DbContextOptions<ResuMetaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<EmployementHistory> EmployementHistories { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ReferenceContactInfo> ReferenceContactInfos { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ResuMetaConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Achievem__3214EC07F29D3DAC");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Achievements).HasConstraintName("Fk Achievements UserInfo Id");
        });

        modelBuilder.Entity<Degree>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Degree__3214EC07F6731261");

            entity.HasOne(d => d.Education).WithMany(p => p.Degrees).HasConstraintName("Fk Degree Education Id");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Educatio__3214EC07323EC1DE");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Educations).HasConstraintName("Fk Education UserInfo Id");
        });

        modelBuilder.Entity<EmployementHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employem__3214EC07D6164316");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.EmployementHistories).HasConstraintName("Fk EmployementHistory UserInfo Id");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC0769D6F1FF");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Projects).HasConstraintName("Fk Projects UserInfo Id");
        });

        modelBuilder.Entity<ReferenceContactInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Referenc__3214EC075F15FFDF");

            entity.HasOne(d => d.EmployementHistory).WithMany(p => p.ReferenceContactInfos).HasConstraintName("Fk ReferenceContactInfo EmployementHistory Id");
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resume__3214EC07E40790C7");

            entity.Property(e => e.Resume1).IsFixedLength();

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Resumes).HasConstraintName("Fk Resume UserInfo Id");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Skills__3214EC072B428F8E");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserInfo__3214EC079DF15E61");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSkil__3214EC076F17DBCD");

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills).HasConstraintName("Fk UserSkill Skill Id");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.UserSkills).HasConstraintName("Fk UserSkill UserInfo Id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
