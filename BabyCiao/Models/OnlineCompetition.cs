using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyCiao.Models;

public partial class OnlineCompetition
{
    public int Id { get; set; }
    [Display(Name = "活動名稱")]
    public string CompetitionName { get; set; } = null!;
    [Display(Name = "修改人員")]
    public string AccountUserAccount { get; set; } = null!;
    [Display(Name = "活動開始時間")]
    public DateOnly StartTime { get; set; }
    [Display(Name = "活動開始時間")]
    public DateOnly EndTime { get; set; }
    [Display(Name = "活動介紹")]
    public string Content { get; set; } = null!;
    [Display(Name = "更新時間")]
    public DateOnly ModifiedTime { get; set; }
    [Display(Name = "活動進行狀態")]
    public string Statement { get; set; } = null!;

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<CompetitionDetail> CompetitionDetails { get; set; } = new List<CompetitionDetail>();

    public virtual ICollection<CompetitionPhoto> CompetitionPhotos { get; set; } = new List<CompetitionPhoto>();
}
