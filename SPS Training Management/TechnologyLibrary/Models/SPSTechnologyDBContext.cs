using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TechnologyLibrary.Models;

public partial class SPSTechnologyDBContext : DbContext
{
    public SPSTechnologyDBContext()
    {
    }

    public SPSTechnologyDBContext(DbContextOptions<SPSTechnologyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Technology> Technologies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("data source=sqlsvrsnrao.database.windows.net; database=SPSTechnologyDB-snrao; user id=snrao; password=Welcome@1234");
        => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=SPSTechnologyDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Technology>(entity =>
        {
            entity.HasKey(e => e.TechId).HasName("PK__Technolo__8AFFB87F21D2F4C0");

            entity.ToTable("Technology");

            entity.Property(e => e.TechId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TechLevel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TechTitle)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
