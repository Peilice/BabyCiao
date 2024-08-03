using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class ProductFormat
{
    public int Id { get; set; }

    public int IdGroupBuying { get; set; }

    public string FormatType { get; set; } = null!;

    public string FormatName { get; set; } = null!;

    public virtual ICollection<GroupBuyingDetailFormat> GroupBuyingDetailFormats { get; set; } = new List<GroupBuyingDetailFormat>();

    public virtual GroupBuying IdGroupBuyingNavigation { get; set; } = null!;
}
