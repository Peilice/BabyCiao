using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class AnnouncementPhoto
{
    public string Id { get; set; } = null!;

    public int IdAnnouncement { get; set; }

    public string PhotoName { get; set; } = null!;

    public string BuiledTime { get; set; } = null!;

    public virtual Announcement IdAnnouncementNavigation { get; set; } = null!;
}
