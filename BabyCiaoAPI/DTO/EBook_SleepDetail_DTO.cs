namespace BabyCiaoAPI.DTO
{
    public class EBook_SleepDetail_DTO
    {
        public string Category { get; set; }
        public int Id { get; set; }

        public int IdContactBook { get; set; }

        public DateTime SleepTime { get; set; }

        public DateTime WakeUpTime { get; set; }

        public string SleepState { get; set; } //= null!;

        public string Content { get; set; } //= null!;

        public string AccountUserAccount { get; set; } //= null!;

        public DateTime ModifiedTime { get; set; }

    }
}
