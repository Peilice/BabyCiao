using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class GroupBuyingFavorite
{
    public int Id { get; set; }

    public int IdGroupBuying { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public virtual GroupBuying IdGroupBuyingNavigation { get; set; } = null!;
}
