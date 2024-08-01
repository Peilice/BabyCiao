using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyCiao.Models;

public partial class Platform
{
    public int Id { get; set; }

    [Display(Name = "發佈者帳號")]
    public string AccountUserAccount { get; set; } = null!;

    [Display(Name = "修改時間")]
    public DateOnly ModifiedTime { get; set; }

    [Display(Name = "文章標題")]
    public string Title { get; set; } = null!;

    [Display(Name = "內文")]
    public string Content { get; set; } = null!;

    [Display(Name = "文章分類")]
    public string Type { get; set; } = null!;

    [Display(Name = "顯示控制")]
    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<PlatformPhoto> PlatformPhotos { get; set; } = new List<PlatformPhoto>();

    public virtual ICollection<PlatformResponse> PlatformResponses { get; set; } = new List<PlatformResponse>();
}
