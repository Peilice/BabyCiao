using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class SystemFunction
{
    public string FunctionCode { get; set; } = null!;

    public string FunctionName { get; set; } = null!;

    public virtual ICollection<PermissionGroup> GroupCodePermissionGroups { get; set; } = new List<PermissionGroup>();
}
