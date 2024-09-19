using System;
using System.Collections.Generic;

namespace SyrmaSGS.Models;

public partial class SubDepartmentMaster
{
    public int SubDepartmentid { get; set; }

    public string? SubDepartmentName { get; set; }

    public int? Departmentid { get; set; }

    public bool? IsActive { get; set; }

    public string? DDpartmentname { get; set; }
}
