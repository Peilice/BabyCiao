using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class CompetitionPhoto
{
    public int Id { get; set; }

    public int IdOnlineCompetition { get; set; }

    public string PhotoName { get; set; } = null!;

    public string ModifiedTime { get; set; } = null!;

    public virtual OnlineCompetition IdOnlineCompetitionNavigation { get; set; } = null!;
}
