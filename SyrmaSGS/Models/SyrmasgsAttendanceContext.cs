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

    public virtual DbSet<EmployeeMasterFull> EmployeeMasterFulls { get; set; }

    public virtual DbSet<EmployeeMasterOld> EmployeeMasterOlds { get; set; }

    public virtual DbSet<SubDepartmentMaster> SubDepartmentMasters { get; set; }

    public virtual DbSet<UnitMaster> UnitMasters { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=STPLPC2072;Database=SyrmasgsAttendance;Integrated Security=True;TrustServerCertificate=True;");

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
            entity
                .HasNoKey()
                .ToTable("DepartMentMaster");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DepartMentName)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.DeptId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DeptID");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Updatedby)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DesignationMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DesignationMaster");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DesiId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DesiID");
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
            entity
                .HasNoKey()
                .ToTable("EmployeeMaster");

            entity.Property(e => e.BranchState).HasMaxLength(255);
            entity.Property(e => e.Category).HasMaxLength(255);
            entity.Property(e => e.CoffPattern).HasMaxLength(255);
            entity.Property(e => e.Company).HasMaxLength(255);
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.DateofConfirmation).HasColumnType("datetime");
            entity.Property(e => e.DateofJoining).HasColumnType("datetime");
            entity.Property(e => e.DateofLastBreak).HasMaxLength(255);
            entity.Property(e => e.DateofLastReJoining).HasMaxLength(255);
            entity.Property(e => e.DateofLeaving).HasColumnType("datetime");
            entity.Property(e => e.DateofProbation).HasMaxLength(255);
            entity.Property(e => e.Department).HasMaxLength(255);
            entity.Property(e => e.Designation).HasMaxLength(255);
            entity.Property(e => e.Division).HasMaxLength(255);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FathersName).HasMaxLength(255);
            entity.Property(e => e.FirstLastPunch).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.FirstPunch).HasMaxLength(255);
            entity.Property(e => e.FixedOffDay).HasMaxLength(255);
            entity.Property(e => e.Grade).HasMaxLength(255);
            entity.Property(e => e.HolidayPattern).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.LateOtadjust)
                .HasMaxLength(255)
                .HasColumnName("LateOTAdjust");
            entity.Property(e => e.LeaveAutoGrant).HasMaxLength(255);
            entity.Property(e => e.LeaveCondition).HasMaxLength(255);
            entity.Property(e => e.LeaveControl).HasMaxLength(255);
            entity.Property(e => e.LeaveReportingTo).HasMaxLength(255);
            entity.Property(e => e.LeaveSecondApproval).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.MiddleName).HasMaxLength(255);
            entity.Property(e => e.OverTime).HasMaxLength(255);
            entity.Property(e => e.PunchStatus).HasMaxLength(255);
            entity.Property(e => e.ReaderGroup).HasMaxLength(255);
            entity.Property(e => e.Rfidcode)
                .HasMaxLength(255)
                .HasColumnName("RFIDCode");
            entity.Property(e => e.Sex).HasMaxLength(255);
            entity.Property(e => e.ShiftDate).HasColumnType("datetime");
            entity.Property(e => e.ShiftPattern).HasMaxLength(255);
            entity.Property(e => e.ShiftsAllowed).HasMaxLength(255);
            entity.Property(e => e.Sno).HasColumnName("SNO");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.StatusFlag)
                .HasMaxLength(255)
                .HasColumnName("Status_Flag");
            entity.Property(e => e.SubDepartment).HasMaxLength(255);
            entity.Property(e => e.SuperDepartment).HasMaxLength(255);
            entity.Property(e => e.UserCreate)
                .HasMaxLength(255)
                .HasColumnName("User_Create");
            entity.Property(e => e.UserCreationDt)
                .HasColumnType("datetime")
                .HasColumnName("User_Creation_Dt");
            entity.Property(e => e.UserMod)
                .HasMaxLength(255)
                .HasColumnName("User_Mod");
            entity.Property(e => e.UserModDt)
                .HasColumnType("datetime")
                .HasColumnName("User_Mod_Dt");
            entity.Property(e => e.WorkstationId)
                .HasMaxLength(255)
                .HasColumnName("Workstation_Id");
        });

        modelBuilder.Entity<EmployeeMasterFull>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmployeeMasterFull");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.Department)
                .HasMaxLength(255)
                .HasColumnName("DEPARTMENT");
            entity.Property(e => e.EmpCode).HasMaxLength(50);
            entity.Property(e => e.EmpName)
                .HasMaxLength(255)
                .HasColumnName("EMP NAME");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("GENDER");
            entity.Property(e => e.SubDepartment)
                .HasMaxLength(255)
                .HasColumnName("SUB DEPARTMENT");
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .HasColumnName("UNIT");
        });

        modelBuilder.Entity<EmployeeMasterOld>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EmployeeMaster_old");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(255)
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.DesignationId)
                .HasMaxLength(255)
                .HasColumnName("DESIGNATION_ID");
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
            entity.Property(e => e.UnitId)
                .HasMaxLength(255)
                .HasColumnName("UNIT_ID");
        });

        modelBuilder.Entity<SubDepartmentMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SubDepartmentMaster");

            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.SubDepartmentName).HasMaxLength(128);
            entity.Property(e => e.SubDepartmentid).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<UnitMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UnitMaster");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Createdby)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UnitID");
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
            entity
                .HasNoKey()
                .ToTable("UserLogin");

            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Createid).HasColumnName("createid");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Updatedate)
                .HasColumnType("datetime")
                .HasColumnName("updatedate");
            entity.Property(e => e.Updateid).HasColumnName("updateid");
            entity.Property(e => e.Userid)
                .HasMaxLength(120)
                .HasColumnName("userid");
            entity.Property(e => e.Userloginid)
                .ValueGeneratedOnAdd()
                .HasColumnName("userloginid");
            entity.Property(e => e.Userpassword)
                .HasMaxLength(125)
                .HasColumnName("userpassword");
            entity.Property(e => e.Usertype)
                .HasMaxLength(25)
                .HasColumnName("usertype");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
