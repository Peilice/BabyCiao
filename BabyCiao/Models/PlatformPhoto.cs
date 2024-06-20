using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class PlatformPhoto
{
    public string Id { get; set; } = null!;

    public int IdPlatform { get; set; }

    public string PhotoName { get; set; } = null!;

    public string ModifiedTime { get; set; } = null!;

    public virtual Platform IdPlatformNavigation { get; set; } = null!;
}
