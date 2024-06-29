namespace BabyCiao.Models.DTO
{
    public class PlatformsDTO
    {
        public int PlatformId { get; set; }
        public string AccountUserAccount { get; set; }
        public DateOnly PlatformModifiedTime { get; set; }
        public string PlatformTitle { get; set; }
        public string PlatformContent { get; set; }
        public string PlatformType { get; set; }
        public bool PlatformDisplay { get; set; }



        public string ResponseContent { get; set; }
        public DateTime ResponseModifiedTime { get; set; }
        public bool ResponseDisplay { get; set; }


    }
}
