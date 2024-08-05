using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class SuppliesPhoto
{
    public int Id { get; set; }

    public int IdSecondHandSupplies { get; set; }

    public string PhotoName { get; set; } = null!;

    public string ModifiedTime { get; set; } = null!;

    public virtual SecondHandSupply IdSecondHandSuppliesNavigation { get; set; } = null!;
}
