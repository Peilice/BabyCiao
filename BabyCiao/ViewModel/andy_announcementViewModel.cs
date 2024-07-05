using BabyCiao.Models;

namespace BabyCiao.ViewModel
{
    public class andy_announcementViewModel
    {
        public int Id { get; set; }
        public string AccountUserAccount { get; set; } = null!;

        public string Tittle { get; set; } = null!;

        public string Article { get; set; } = null!;

        public string ReferenceName { get; set; } = null!;

        public string ReferenceRoute { get; set; } = null!;

        public string Type { get; set; }

        public bool Display { get; set; }

        public string? Picture { get; set; }
    }
}
