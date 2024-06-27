using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class AuthGroup
{
    public int GroupId { get; set; }

    public string GroupDescription { get; set; } = null!;

    public string ModifiedPersonUserAccount { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<FunctionSetting> FunctionSettings { get; set; } = new List<FunctionSetting>();

    public virtual UserAccount ModifiedPersonUserAccountNavigation { get; set; } = null!;
}
