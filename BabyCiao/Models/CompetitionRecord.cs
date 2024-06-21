using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class CompetitionRecord
{
    public string VoterAccount { get; set; } = null!;

    public int IdCompetitionDetail { get; set; }

    public DateTime ModifiedTime { get; set; }

    public virtual CompetitionDetail IdCompetitionDetailNavigation { get; set; } = null!;
}
