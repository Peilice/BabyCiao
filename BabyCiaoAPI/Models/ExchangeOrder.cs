using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class ExchangeOrder
{
    public int Id { get; set; }

    public string AccountAUserAccount { get; set; } = null!;

    public string AccountBUserAccount { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public string Statement { get; set; } = null!;

    public virtual UserAccount AccountAUserAccountNavigation { get; set; } = null!;

    public virtual UserAccount AccountBUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<ExchangeOrderDetail> ExchangeOrderDetails { get; set; } = new List<ExchangeOrderDetail>();
}
