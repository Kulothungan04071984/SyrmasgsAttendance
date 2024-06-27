using System;
using System.Collections.Generic;

namespace SyrmaSGS.Models;

public partial class EmployeeMaster
{
    public double? SNo { get; set; }

    public string? Employeeid { get; set; }

    public string? Employeename { get; set; }

    public string? Dob { get; set; }

    public string? Doj { get; set; }

    public string? Gender { get; set; }

    public string? CategoryName { get; set; }

    public string? DesignationName { get; set; }

    public string? DepartmentName { get; set; }

    public string? EMailId { get; set; }

    public string? Unit { get; set; }

    public double? ReportingManagerId { get; set; }

    public string? ReportingManagerName { get; set; }

    public string? ReportingManagerEMailId { get; set; }
}
