﻿using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class CompetitionDetail
{
    public int Id { get; set; }

    public int IdOnlineCompetition { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public string? CompetitionPhoto { get; set; }

    public string Content { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public virtual OnlineCompetition IdOnlineCompetitionNavigation { get; set; } = null!;
}
