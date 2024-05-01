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

    public virtual DbSet<ApplicationTracker> ApplicationTrackers { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<Education> Educations { get; set; }

    public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ReferenceContactInfo> ReferenceContactInfos { get; set; }

    public virtual DbSet<Resume> Resumes { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }
    public virtual DbSet<CoverLetter> CoverLetters { get; set; }
    public virtual DbSet<ResumeTemplate> ResumeTemplates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Name=ResuMetaConnection");
        }
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Achievem__3214EC0771090A2C");

            entity.HasOne(d => d.Resume).WithMany(p => p.Achievements).HasConstraintName("Fk Achievements Resume Id");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Achievements).HasConstraintName("Fk Achievements UserInfo Id");
        });

        modelBuilder.Entity<ApplicationTracker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC0758C85EC9");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.ApplicationTrackers).HasConstraintName("Fk ApplicationTracker UserInfo Id");
        });

        modelBuilder.Entity<Degree>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Degree__3214EC078DF0E469");

            entity.HasOne(d => d.Education).WithMany(p => p.Degrees).HasConstraintName("Fk Degree Education Id");
        });

        modelBuilder.Entity<Education>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Educatio__3214EC07F870F0E0");

            entity.HasOne(d => d.Resume).WithMany(p => p.Educations).HasConstraintName("Fk Education Resume Id");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Educations).HasConstraintName("Fk Education UserInfo Id");
        });

        modelBuilder.Entity<EmploymentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employme__3214EC0709675694");

            entity.HasOne(d => d.Resume).WithMany(p => p.EmploymentHistories).HasConstraintName("Fk EmploymentHistory Resume Id");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.EmploymentHistories).HasConstraintName("Fk EmploymentHistory UserInfo Id");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC0735A54740");

            entity.HasOne(d => d.Resume).WithMany(p => p.Projects).HasConstraintName("Fk Projects Resume Id");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Projects).HasConstraintName("Fk Projects UserInfo Id");
        });

        modelBuilder.Entity<ReferenceContactInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Referenc__3214EC07F4C23CC2");

            entity.HasOne(d => d.EmploymentHistory).WithMany(p => p.ReferenceContactInfos).HasConstraintName("Fk ReferenceContactInfo EmploymentHistory Id");
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resume__3214EC076672136E");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Resumes).HasConstraintName("Fk Resume UserInfo Id");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Skills__3214EC070E2C2AFC");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserInfo__3214EC07B47DD646");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSkil__3214EC079D65A9F2");

            entity.HasOne(d => d.Resume).WithMany(p => p.UserSkills).HasConstraintName("Fk UserSkill Resume Id");

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills).HasConstraintName("Fk UserSkill Skill Id");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.UserSkills).HasConstraintName("Fk UserSkill UserInfo Id");
        });

        modelBuilder.Entity<CoverLetter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CoverLet__3214EC07A3A3D3A3");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.CoverLetters).HasConstraintName("Fk CoverLetter UserInfo Id");
        });

        modelBuilder.Entity<ResumeTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ResumeTe__3214EC07A3A3D3A3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
