namespace BabyCiaoAPI.DTO
{
    public class getAnnouncement_DTO
    {
        public int id { get; set; }
        public DateTime PublishTime { get; set; }
        public string Tittle { get; set; }
        public string Type { get; set; } //= null!;
    }
}
