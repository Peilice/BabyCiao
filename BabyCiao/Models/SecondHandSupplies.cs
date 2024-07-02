using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class SecondHandSupplies
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

    public virtual ICollection<ExchangeOrderDetail> ExchangeOrderDetails { get; set; } = new List<ExchangeOrderDetail>();

    public virtual ICollection<SuppliesPhoto> SuppliesPhotos { get; set; } = new List<SuppliesPhoto>();
}
