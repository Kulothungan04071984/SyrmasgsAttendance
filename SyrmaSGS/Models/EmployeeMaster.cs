using System;
using System.Collections.Generic;

namespace SyrmaSGS.Models;

public partial class EmployeeMaster
{
    public double? SNo { get; set; }

    public string? EmpCode { get; set; }

    public string? Empname { get; set; }

    public DateTime? Dob { get; set; }

    public DateTime? Doj { get; set; }

    public string? Gender { get; set; }

    public string? DesigName { get; set; }

    public string? DepartmentName { get; set; }

    public string? SubDepartment { get; set; }

    public string? Unit { get; set; }

    public string? CategoryName { get; set; }
}
