using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class GroupBuyingPhoto
{
    public string Id { get; set; } = null!;

    public int IdGroupBuying { get; set; }

    public string PhotoName { get; set; } = null!;

    public string ModifiedTime { get; set; } = null!;

    public virtual GroupBuying IdGroupBuyingNavigation { get; set; } = null!;
}
