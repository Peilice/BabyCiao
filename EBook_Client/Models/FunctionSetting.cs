using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class FunctionSetting
{
    public int GroupIdAuthGroup { get; set; }

    public int FunctionCodeSystemFunction { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual SystemFunction FunctionCodeSystemFunctionNavigation { get; set; } = null!;

    public virtual AuthGroup GroupIdAuthGroupNavigation { get; set; } = null!;
}
