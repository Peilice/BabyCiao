using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class SecondHandExchangeOrderDetail
{
    public int IdExchangeOrder { get; set; }

    public bool BuyerOk { get; set; }

    public bool SellerOk { get; set; }

    public virtual SecondHandExchangeOrder IdExchangeOrderNavigation { get; set; } = null!;
}
