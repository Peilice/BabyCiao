using System;
using System.Collections.Generic;

namespace BabyCiao_Client.Models;

public partial class AnnouncementPhoto
{
    public int Id { get; set; }

    public int IdAnnouncement { get; set; }

    public string PhotoName { get; set; } = null!;

    public DateTime BuiledTime { get; set; }

    public virtual Announcement IdAnnouncementNavigation { get; set; } = null!;
}
