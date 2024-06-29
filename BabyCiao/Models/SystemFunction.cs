using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class SystemFunction
{
    public int FunctionId { get; set; }

    public string FunctionName { get; set; } = null!;

    public virtual ICollection<FunctionSetting> FunctionSettings { get; set; } = new List<FunctionSetting>();
}
