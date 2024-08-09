using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class PlatformFavorite
{
    public int Id { get; set; }

    public int IdPlatform { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public virtual Platform IdPlatformNavigation { get; set; } = null!;
}
