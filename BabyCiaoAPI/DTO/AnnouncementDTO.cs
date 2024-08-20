using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

public class AnnouncementWithPhotosDTO
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

    public List<AnnouncementPhotoDTO> Photos { get; set; } = new List<AnnouncementPhotoDTO>();
}

public partial class AnnouncementDTO
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

}

public partial class AnnouncementPhotoDTO
{
    public int Id { get; set; }

    public int IdAnnouncement { get; set; }

    public string PhotoName { get; set; } = null!;

    public DateTime BuiledTime { get; set; }

}

