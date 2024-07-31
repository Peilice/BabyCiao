using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class ExchangeOrderDetail
{
    public int IdExchangeOrder { get; set; }

    public int IdSecondHandSupplies { get; set; }

    public int Quantity { get; set; }

    public virtual ExchangeOrder IdExchangeOrderNavigation { get; set; } = null!;

    public virtual SecondHandSupply IdSecondHandSuppliesNavigation { get; set; } = null!;
}
