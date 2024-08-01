namespace BabyCiaoAPI.DTO
{
    public class EBook_Memo_DTO
    {
        public int Id { get; set; }

        public int IdContactBook { get; set; }

        public DateTime RecodeTime { get; set; }

        public string Memo1 { get; set; } = null!;

        public string AccountUserAccount { get; set; } = null!;

        public DateTime ModifiedTime { get; set; }
    }
}
