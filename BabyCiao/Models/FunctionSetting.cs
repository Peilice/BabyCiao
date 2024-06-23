using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class FunctionSetting
{
    public int GroupCodePermissionGroup { get; set; }

    public string FunctionCodeSystemFunction { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual SystemFunction FunctionCodeSystemFunctionNavigation { get; set; } = null!;

    public virtual PermissionGroup GroupCodePermissionGroupNavigation { get; set; } = null!;
}
