using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class Platform
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public DateOnly ModifiedTime { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<PlatformPhoto> PlatformPhotos { get; set; } = new List<PlatformPhoto>();

    public virtual ICollection<PlatformResponse> PlatformResponses { get; set; } = new List<PlatformResponse>();
}
