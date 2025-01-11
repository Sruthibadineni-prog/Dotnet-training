using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrainingLibrary.Models;

public partial class SPSTrainingDBContext : DbContext
{
    public SPSTrainingDBContext()
    {
    }

    public SPSTrainingDBContext(DbContextOptions<SPSTrainingDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Technology> Technologies { get; set; }

    public virtual DbSet<Trainee> Trainees { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    public virtual DbSet<Training> Training { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=SPSTrainingDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB997526F298");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Technology>(entity =>
        {
            entity.HasKey(e => e.TechId).HasName("PK__Technolo__8AFFB87F9C11434F");

            entity.ToTable("Technology");

            entity.Property(e => e.TechId)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Trainee>(entity =>
        {
            entity.HasKey(e => new { e.TrainingId, e.TraineeId }).HasName("PK__Trainee__5B6D8C9EEAC3BA91");

            entity.ToTable("Trainee");

            entity.Property(e => e.TrainingId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TraineeId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TraineeStatus)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.HasOne(d => d.TraineeNavigation).WithMany(p => p.Trainees)
                .HasForeignKey(d => d.TraineeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trainee__Trainee__412EB0B6");

            entity.HasOne(d => d.Training).WithMany(p => p.Trainees)
                .HasForeignKey(d => d.TrainingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Trainee__Trainin__403A8C7D");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__Trainer__366A1A7C7FFF08A1");

            entity.ToTable("Trainer");

            entity.Property(e => e.TrainerId)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Training>(entity =>
        {
            entity.HasKey(e => e.TrainingId).HasName("PK__Training__E8D71D826653FEA5");

            entity.Property(e => e.TrainingId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.TechId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TrainerId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Tech).WithMany(p => p.Training)
                .HasForeignKey(d => d.TechId)
                .HasConstraintName("FK__Training__TechId__3C69FB99");

            entity.HasOne(d => d.Trainer).WithMany(p => p.InverseTrainer)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK__Training__Traine__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
