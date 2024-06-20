using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class ExchangeOrder
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public int Statement { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<ExchangeOrderDetail> ExchangeOrderDetails { get; set; } = new List<ExchangeOrderDetail>();
}
