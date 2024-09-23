using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SyrmaSGS.Models;

public partial class SyrmasgsAttendanceContext : DbContext
{
    public SyrmasgsAttendanceContext()
    {
    }

    public SyrmasgsAttendanceContext(DbContextOptions<SyrmasgsAttendanceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AttendanceTransactionDetail> AttendanceTransactionDetails { get; set; }

    public virtual DbSet<DepartMentMaster> DepartMentMasters { get; set; }

    public virtual DbSet<DesignationMaster> DesignationMasters { get; set; }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    public virtual DbSet<SubDepartmentMaster> SubDepartmentMasters { get; set; }

    public virtual DbSet<UnitMaster> UnitMasters { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.1.146;Database=SyrmasgsAttendance;user id=sa;password=syrma@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttendanceTransactionDetail>(entity =>
        {
            entity.HasKey(e => e.TransId);

            entity.Property(e => e.TransId).HasColumnName("TransID");
            entity.Property(e => e.CurrentDate).HasColumnType("datetime");
            entity.Property(e => e.DepartMemtId).HasColumnName("DepartMemtID");
            entity.Property(e => e.DesignationId).HasColumnName("DesignationID");
            entity.Property(e => e.EmpId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EmpID");
            entity.Property(e => e.EmpName)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.UnitId).HasColumnName("UnitID");
        });

        modelBuilder.Entity<DepartMentMaster>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__DepartMe__0148818EF6B2C675");

            entity.ToTable("DepartMentMaster");

            entity.Property(e => e.DeptId).HasColumnName("DeptID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DepartMentName)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DesignationMaster>(entity =>
        {
            entity.HasKey(e => e.DesiId).HasName("PK__Designat__F6D47B909C858D37");

            entity.ToTable("DesignationMaster");

            entity.Property(e => e.DesiId).HasColumnName("DesiID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DesignationName)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity.HasKey(e => e.Employeemasterid).HasName("PK__Employee__531F07A262E66C1B");

            entity.ToTable("EmployeeMaster");

            entity.Property(e => e.Employeemasterid).HasColumnName("EMPLOYEEMASTERID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(255)
                .HasColumnName("DEPARTMENT_NAME");
            entity.Property(e => e.DesigName)
                .HasMaxLength(255)
                .HasColumnName("DESIG_NAME");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Doj)
                .HasColumnType("datetime")
                .HasColumnName("DOJ");
            entity.Property(e => e.Empcode)
                .HasMaxLength(255)
                .HasColumnName("EMPCODE");
            entity.Property(e => e.Empname)
                .HasMaxLength(255)
                .HasColumnName("EMPNAME");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("GENDER");
            entity.Property(e => e.Isactive).HasColumnName("ISACTIVE");
            entity.Property(e => e.Subdepartment)
                .HasMaxLength(255)
                .HasColumnName("SUBDEPARTMENT");
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .HasColumnName("UNIT");
        });

        modelBuilder.Entity<SubDepartmentMaster>(entity =>
        {
            entity.HasKey(e => e.SubDepartmentid).HasName("PK__SubDepar__17B3338E52D9DB99");

            entity.ToTable("SubDepartmentMaster");

            entity.Property(e => e.DDpartmentname)
                .HasMaxLength(125)
                .IsUnicode(false)
                .HasColumnName("D_Dpartmentname");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.SubDepartmentName).HasMaxLength(128);
        });

        modelBuilder.Entity<UnitMaster>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__UnitMast__44F5EC957793D2A5");

            entity.ToTable("UnitMaster");

            entity.Property(e => e.UnitId).HasColumnName("UnitID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.ToTable("UserLogin");

            entity.Property(e => e.Userid).HasMaxLength(125);
            entity.Property(e => e.Userpassword).HasMaxLength(125);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
