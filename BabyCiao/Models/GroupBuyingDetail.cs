using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class GroupBuyingDetail
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime ModifiedTime { get; set; }

    public int Statement { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;
}
