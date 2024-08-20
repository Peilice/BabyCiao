using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class Inquire
{
    public int Id { get; set; }

    public string UserAccountresponse { get; set; } = null!;

    public string UserAccountinquire { get; set; } = null!;

    public int ContentId { get; set; }

    public string? ContentTitle { get; set; }

    public string? Content { get; set; }

    public DateTime? Createdtime { get; set; }

    public virtual UserAccount UserAccountinquireNavigation { get; set; } = null!;

    public virtual UserAccount UserAccountresponseNavigation { get; set; } = null!;
}
