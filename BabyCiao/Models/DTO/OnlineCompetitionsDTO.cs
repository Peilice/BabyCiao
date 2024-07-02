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
        public string? PhotoName { get; set; }

    }

    public static class CompetitionsExtensions
    {
        public static OnlineCompetition ToEntity(this OnlineCompetitionsDTO dto)
        {
            return new OnlineCompetition
            {
                Id = dto.Id,
                AccountUserAccount = dto.AccountUserAccount,
                ModifiedTime = dto.ModifiedTime,
                Content = dto.Content,
                Statement = dto.Statement,
            };
        }

        public static void UpdateEntity(this OnlineCompetition entity, OnlineCompetitionsDTO dto)
        {
            entity.Id = dto.Id;
            entity.AccountUserAccount = dto.AccountUserAccount;
            entity.ModifiedTime = dto.ModifiedTime;
            entity.Content = dto.Content;
            entity.Statement = dto.Statement;
        }
    }

}
