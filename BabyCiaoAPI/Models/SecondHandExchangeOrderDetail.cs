using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class SecondHandExchangeOrderDetail
{
    public int IdExchangeOrder { get; set; }

    public bool BuyerOk { get; set; }

    public bool SellerOk { get; set; }

    public virtual SecondHandExchangeOrder IdExchangeOrderNavigation { get; set; } = null!;
}
