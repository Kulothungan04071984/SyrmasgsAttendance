using System;
using System.Collections.Generic;

namespace SyrmaSGS.Models;

public partial class AttendanceTransactionDetail
{
    public string? EmpId { get; set; }

    public string? EmpName { get; set; }

    public int? UnitId { get; set; }

    public int? DepartMemtId { get; set; }

    public int? DesignationId { get; set; }

    public DateTime? CurrentDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool? IsActive { get; set; }

    public int TransId { get; set; }
}
