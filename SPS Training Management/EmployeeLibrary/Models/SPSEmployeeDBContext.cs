using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLibrary.Models;

public partial class SPSEmployeeDBContext : DbContext
{
    public SPSEmployeeDBContext()
    {
    }

    public SPSEmployeeDBContext(DbContextOptions<SPSEmployeeDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("data source=sqlsvrsnrao.database.windows.net; database=SPSEmployeeDB-snrao; user id=snrao; password=Welcome@1234");
       => optionsBuilder.UseSqlServer("data source=(localdb)\\MSSQLLocalDB; database=SPSEmployeeDB; integrated security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB99F9039AFC");

            entity.ToTable("Employee");

            entity.Property(e => e.EmpId).ValueGeneratedNever();
            entity.Property(e => e.EmpEmail)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.EmpName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.EmpPhone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
