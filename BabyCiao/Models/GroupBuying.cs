using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class GroupBuying
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public int TargetCount { get; set; }

    public int Price { get; set; }

    public string Statement { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public string? ProductType { get; set; }

    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<GroupBuyingDetail> GroupBuyingDetails { get; set; } = new List<GroupBuyingDetail>();

    public virtual ICollection<GroupBuyingPhoto> GroupBuyingPhotos { get; set; } = new List<GroupBuyingPhoto>();

    public virtual ICollection<ProductFormat> ProductFormats { get; set; } = new List<ProductFormat>();
}
