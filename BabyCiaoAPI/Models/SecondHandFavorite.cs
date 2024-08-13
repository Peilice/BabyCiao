using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class SecondHandFavorite
{
    public int Id { get; set; }

    public int IdSecondHandSupplies { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public virtual SecondHandSupply IdSecondHandSuppliesNavigation { get; set; } = null!;
}
