using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class PlatformResponse
{
    public int Id { get; set; }

    public int IdPlatform { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public bool Display { get; set; }

    public virtual Platform IdPlatformNavigation { get; set; } = null!;
}
