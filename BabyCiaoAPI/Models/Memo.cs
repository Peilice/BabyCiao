using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class Memo
{
    public int Id { get; set; }

    public int IdContactBook { get; set; }

    public DateTime RecodeTime { get; set; }

    public string Memo1 { get; set; } = null!;

    public string AccountUserAccount { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public virtual ContactBook IdContactBookNavigation { get; set; } = null!;
}
