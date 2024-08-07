using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class CompetitionFavorite
{
    public int Id { get; set; }

    public int IdOnlineCompetition { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public virtual OnlineCompetition IdOnlineCompetitionNavigation { get; set; } = null!;
}
