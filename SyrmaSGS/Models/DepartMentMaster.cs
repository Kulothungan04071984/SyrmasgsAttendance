using System;
using System.Collections.Generic;

namespace SyrmaSGS.Models;

public partial class DepartMentMaster
{
    public int DeptId { get; set; }

    public string? DepartMentName { get; set; }

    public string? Createdby { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Updatedby { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsActive { get; set; }
}
