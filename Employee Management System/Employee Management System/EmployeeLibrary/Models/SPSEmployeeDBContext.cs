using System;
using System.Collections.Generic;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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
    {
        SecretClientOptions options = new SecretClientOptions() {
          Retry = {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
          }
        };
        var client = new SecretClient(new Uri("https://mykeyvault-snrao.vault.azure.net/"), new DefaultAzureCredential(), options);
        KeyVaultSecret secret = client.GetSecret("spsempdbpwd");
        string secretValue = secret.Value;
        optionsBuilder.UseSqlServer("data source=sqlsvrsnrao.database.windows.net; database=SPSEmployeeDB-snrao; user id=snrao; password=" + secretValue);
    }

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
