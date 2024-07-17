using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class OnlineCompetition
{
    public int Id { get; set; }

    public string CompetitionName { get; set; } = null!;

    public string AccountUserAccount { get; set; } = null!;

    public DateOnly StartTime { get; set; }

    public DateOnly EndTime { get; set; }

    public string Content { get; set; } = null!;

    public DateOnly ModifiedTime { get; set; }

    public string Statement { get; set; } = null!;

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<CompetitionDetail> CompetitionDetails { get; set; } = new List<CompetitionDetail>();

    public virtual ICollection<CompetitionPhoto> CompetitionPhotos { get; set; } = new List<CompetitionPhoto>();
}
