using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class SecondHandExchangeOrder
{
    public int Id { get; set; }

    public string BuyerId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public int WantGetId { get; set; }

    public int GetQuantity { get; set; }

    public int WantGiveId { get; set; }

    public int GiveQuantity { get; set; }

    public DateTime ModifiedTime { get; set; }

    public string Statement { get; set; } = null!;

    public virtual UserAccount Buyer { get; set; } = null!;

    public virtual UserAccount Seller { get; set; } = null!;

    public virtual SecondHandSupply WantGet { get; set; } = null!;

    public virtual SecondHandSupply WantGive { get; set; } = null!;
}
