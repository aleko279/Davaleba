using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Davaleba.Models;

public partial class DavalebaContext : DbContext
{
    public DavalebaContext()
    {
    }

    public DavalebaContext(DbContextOptions<DavalebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LkploanStatus> LkploanStatuses { get; set; }

    public virtual DbSet<LkploanType> LkploanTypes { get; set; }

    public virtual DbSet<LoanApplication> LoanApplications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Davaleba;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LkploanStatus>(entity =>
        {
            entity.ToTable("LKPLoanStatus");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LkploanType>(entity =>
        {
            entity.ToTable("LKPLoanType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LoanApplication>(entity =>
        {
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.Period).HasMaxLength(50);

            entity.HasOne(d => d.LoanType).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.LoanTypeId)
                .HasConstraintName("FK_LoanApplications_LKPLoanType");

            entity.HasOne(d => d.Status).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_LoanApplications_LKPLoanStatus");

            entity.HasOne(d => d.User).WithMany(p => p.LoanApplications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_LoanApplications_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.BirthDate).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
