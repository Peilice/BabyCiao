using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class InquireDTO
{ 
    public string UserAccountresponse { get; set; } = null!;

    public string UserAccountinquire { get; set; } = null!;

    public int Times { get; set; }

    public virtual UserAccount UserAccountinquireNavigation { get; set; } = null!;

    public virtual UserAccount UserAccountresponseNavigation { get; set; } = null!;
}
