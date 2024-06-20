using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class DiaperDetail
{
    public int Id { get; set; }

    public int IdContactBook { get; set; }

    public DateTime RecodeTime { get; set; }

    public int Content { get; set; }

    public string BowelSituation { get; set; } = null!;

    public string AccountUserAccount { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public virtual ContactBook IdContactBookNavigation { get; set; } = null!;
}
