using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class GroupBuyingDetailFormat
{
    public int Id { get; set; }

    public int GroupBuyingDetailId { get; set; }

    public int FormatId { get; set; }

    public virtual ProductFormat Format { get; set; } = null!;

    public virtual GroupBuyingDetail GroupBuyingDetail { get; set; } = null!;
}
