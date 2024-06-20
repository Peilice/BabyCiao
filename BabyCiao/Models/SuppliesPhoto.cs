using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class SuppliesPhoto
{
    public string Id { get; set; } = null!;

    public int IdSecondHandSupplies { get; set; }

    public string PhotoName { get; set; } = null!;

    public string ModifiedTime { get; set; } = null!;

    public virtual SecondHandSupply IdSecondHandSuppliesNavigation { get; set; } = null!;
}
