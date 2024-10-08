using System;
using System.Collections.Generic;

namespace SyrmaSGS.Models;

public partial class EmployeeTest
{
    public int Employeemasterid { get; set; }

    public string? Empcode { get; set; }

    public string? Empname { get; set; }

    public DateTime? Dob { get; set; }

    public DateTime? Doj { get; set; }

    public string? Gender { get; set; }

    public int? DesigName { get; set; }

    public int? DepartmentName { get; set; }

    public int? Subdepartment { get; set; }

    public int? Unit { get; set; }

    public string? CategoryName { get; set; }

    public bool? Isactive { get; set; }
}
