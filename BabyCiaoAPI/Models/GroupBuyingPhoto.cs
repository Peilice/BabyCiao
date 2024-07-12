using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class GroupBuyingPhoto
{
    public int Id { get; set; }

    public int IdGroupBuying { get; set; }

    public string PhotoName { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public virtual GroupBuying IdGroupBuyingNavigation { get; set; } = null!;
}
