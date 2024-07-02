using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class ExchangeOrderDetail
{
    public int IdExchangeOrder { get; set; }

    public int IdSecondHandSupplies { get; set; }

    public int Quantity { get; set; }

    public virtual ExchangeOrder IdExchangeOrderNavigation { get; set; } = null!;

    public virtual SecondHandSupplies IdSecondHandSuppliesNavigation { get; set; } = null!;
}
