using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class SleepDetail
{
    public int Id { get; set; }

    public int IdContactBook { get; set; }

    public DateTime SleepTime { get; set; }

    public DateTime WakeUpTime { get; set; }

    public string SleepState { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string AccountUserAccount { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public virtual ContactBook IdContactBookNavigation { get; set; } = null!;
}
