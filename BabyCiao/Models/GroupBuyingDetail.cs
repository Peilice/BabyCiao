using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class GroupBuyingDetail
{
    public int Id { get; set; }

    public int GroupBuyingId { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime ModifiedTime { get; set; }

    public string Statement { get; set; } = null!;

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual GroupBuying GroupBuying { get; set; } = null!;

    public virtual ICollection<GroupBuyingDetailFormat> GroupBuyingDetailFormats { get; set; } = new List<GroupBuyingDetailFormat>();
}
