using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class GroupBuyingDetailFormat
{
    public int Id { get; set; }

    public int GroupBuyingDetailId { get; set; }

    public int? FormatId { get; set; }

    public int Quantity { get; set; }

    public virtual ProductFormat? Format { get; set; }

    public virtual GroupBuyingDetail GroupBuyingDetail { get; set; } = null!;
}
