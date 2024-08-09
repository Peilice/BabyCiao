using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class CompetitionPhoto
{
    public int Id { get; set; }

    public int IdOnlineCompetition { get; set; }

    public string? PhotoName { get; set; }

    public DateTime ModifiedTime { get; set; }

    public virtual OnlineCompetition IdOnlineCompetitionNavigation { get; set; } = null!;
}
