using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class GroupBuying
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public int Statement { get; set; }

    public DateTime ModifiedTime { get; set; }

    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<GroupBuyingPhoto> GroupBuyingPhotos { get; set; } = new List<GroupBuyingPhoto>();
}
