using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class CompetitionFavorite
{
    public int Id { get; set; }

    public int IdOnlineCompetition { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public virtual OnlineCompetition IdOnlineCompetitionNavigation { get; set; } = null!;
}
