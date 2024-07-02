using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class PlatformPhoto
{
    public int Id { get; set; }

    public int IdPlatform { get; set; }

    public string PhotoName { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public virtual Platform IdPlatformNavigation { get; set; } = null!;
}
