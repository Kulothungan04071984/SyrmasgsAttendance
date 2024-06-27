using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SyrmaSGS.Models;

public partial class SyrmasgsAttendanceContext : DbContext
{
    
    public SyrmasgsAttendanceContext(DbContextOptions<SyrmasgsAttendanceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=STPLPC2072;Initial Catalog=SyrmasgsAttendance;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmployeeMaster");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(255)
                .HasColumnName("DEPARTMENT_NAME");
            entity.Property(e => e.DesignationName)
                .HasMaxLength(255)
                .HasColumnName("DESIGNATION_NAME");
            entity.Property(e => e.Dob)
                .HasMaxLength(255)
                .HasColumnName("DOB");
            entity.Property(e => e.Doj)
                .HasMaxLength(255)
                .HasColumnName("DOJ");
            entity.Property(e => e.EMailId)
                .HasMaxLength(255)
                .HasColumnName("E-MAIL ID");
            entity.Property(e => e.Employeeid)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("EMPLOYEEID");
            entity.Property(e => e.Employeename)
                .HasMaxLength(255)
                .HasColumnName("EMPLOYEENAME");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("GENDER");
            entity.Property(e => e.ReportingManagerEMailId)
                .HasMaxLength(255)
                .HasColumnName("REPORTING MANAGER E-MAIL ID");
            entity.Property(e => e.ReportingManagerId).HasColumnName("REPORTING MANAGER ID");
            entity.Property(e => e.ReportingManagerName)
                .HasMaxLength(255)
                .HasColumnName("REPORTING MANAGER NAME");
            entity.Property(e => e.SNo).HasColumnName("S.NO");
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .HasColumnName("UNIT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
