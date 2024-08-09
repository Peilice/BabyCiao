using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class CompetitionRecord
{
    public int Id { get; set; }

    public int IdOnlineCompetition { get; set; }

    public string VoterAccount { get; set; } = null!;

    public int IdCompetitionDetail { get; set; }

    public DateTime ModifiedTime { get; set; }

    public virtual OnlineCompetition IdOnlineCompetitionNavigation { get; set; } = null!;
}
