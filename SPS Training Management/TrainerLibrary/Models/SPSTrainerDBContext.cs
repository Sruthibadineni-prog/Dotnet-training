using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrainerLibrary.Models;

public partial class SPSTrainerDBContext : DbContext
{
    public SPSTrainerDBContext()
    {
    }

    public SPSTrainerDBContext(DbContextOptions<SPSTrainerDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=SPSTrainerDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__Trainer__366A1A7CA65F0004");

            entity.ToTable("Trainer");

            entity.Property(e => e.TrainerId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TrainerEmail)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TrainerName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TrainerPhone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TrainerType)
                .HasMaxLength(3)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
