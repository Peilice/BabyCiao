using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class Vip
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public DateOnly StartTime { get; set; }

    public DateOnly EndTime { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;
}
