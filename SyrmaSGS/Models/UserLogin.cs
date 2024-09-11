using System;
using System.Collections.Generic;

namespace SyrmaSGS.Models;

public partial class UserLogin
{
    public int UserLoginId { get; set; }

    public string? Userid { get; set; }

    public string? Userpassword { get; set; }

    public bool? Isactive { get; set; }
}
