using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class Announcement
{
    public int Id { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public DateTime PublishTime { get; set; }

    public string Tittle { get; set; } = null!;

    public string Article { get; set; } = null!;

    public string ReferenceName { get; set; } = null!;

    public string ReferenceRoute { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual ICollection<AnnouncementPhoto> AnnouncementPhotos { get; set; } = new List<AnnouncementPhoto>();
}
