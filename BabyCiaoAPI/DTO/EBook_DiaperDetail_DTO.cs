namespace BabyCiaoAPI.DTO
{
    public class EBook_DiaperDetail_DTO
    {
        public string Category { get; set; }
        public int Id { get; set; }

        public int IdContactBook { get; set; }

        public DateTime RecodeTime { get; set; }

        public int Content { get; set; }

        public string BowelSituation { get; set; } //= null!;

        public string AccountUserAccount { get; set; } //= null!;

        public DateTime ModifiedTime { get; set; }
    }
}
