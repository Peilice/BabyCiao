using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class DietDetail
{
    public int Id { get; set; }

    public int IdContactBook { get; set; }

    public DateTime RecodeTime { get; set; }

    public string Type { get; set; } = null!;

    public int Amount { get; set; }

    public int Quantity { get; set; }

    public string ModifiedTime { get; set; } = null!;

    public string AccountUserAccount { get; set; } = null!;

    public virtual ContactBook IdContactBookNavigation { get; set; } = null!;
}
