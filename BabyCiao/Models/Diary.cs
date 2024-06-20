using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class Diary
{
    public int Id { get; set; }

    public int IdContactBook { get; set; }

    public int AccountUserAccount { get; set; }

    public string Photo { get; set; } = null!;

    public string Memo { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public bool Display { get; set; }

    public virtual ContactBook IdContactBookNavigation { get; set; } = null!;
}
