using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class PermissionGroup
{
    public int GroupCode { get; set; }

    public string GroupDescription { get; set; } = null!;

    public string ModifiedPersonUserAccount { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual UserAccount ModifiedPersonUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<SystemFunction> FunctionCodeSystemFunctions { get; set; } = new List<SystemFunction>();
}
