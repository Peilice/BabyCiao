using BabyCiao.Models;

namespace BabyCiao.Models.DTO
{
    public class OnlineCompetitionsDTO
    {
        public int Id { get; set; }
        public string CompetitionName { get; set; }
        public string AccountUserAccount { get; set; }
        public DateOnly StartTime { get; set; }
        public DateOnly EndTime { get; set; }
        public string Content { get; set; }
        public string Statement { get; set; }
        public DateOnly ModifiedTime { get; set; }
        public string PhotoName { get; set; }

    }
}
