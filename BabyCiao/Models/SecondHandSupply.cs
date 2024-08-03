using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class SecondHandSupply
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public string SuppliesName { get; set; } = null!;

    public string SuppliesDescription { get; set; } = null!;

    public int StockQuantity { get; set; }

    public DateTime ModifiedTime { get; set; }

    public string Type { get; set; } = null!;

    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<SecondHandExchangeOrder> SecondHandExchangeOrderWantGets { get; set; } = new List<SecondHandExchangeOrder>();

    public virtual ICollection<SecondHandExchangeOrder> SecondHandExchangeOrderWantGives { get; set; } = new List<SecondHandExchangeOrder>();

    public virtual ICollection<SuppliesPhoto> SuppliesPhotos { get; set; } = new List<SuppliesPhoto>();
}
