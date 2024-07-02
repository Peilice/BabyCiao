using Azure;

namespace BabyCiao.Models.DTO
{
    public class PlatformsDTO
    {
        public int PlatformId { get; set; }
        public string PlatformAccountUserAccount { get; set; }
        public DateOnly PlatformModifiedTime { get; set; }
        public string PlatformTitle { get; set; }
        public string PlatformContent { get; set; }
        public string PlatformType { get; set; }
        public bool PlatformDisplay { get; set; }
        public IEnumerable<Response>? Responses { get; set; }


        public class Response
        {
            public int ResponseId { get; set; }
            public DateTime ResponseModifiedTime { get; set; }
            public string ResponseContent { get; set; }
            public bool ResponseDisplay { get; set; }
            public string ResponseAccountUserAccount { get; set; }
        }

    }

    public static class PlatformsExtensions
    {
        public static Platform ToEntity(this PlatformsDTO dto)
        {
            return new Platform
            {
                Id = dto.PlatformId,
                AccountUserAccount = dto.PlatformAccountUserAccount,
                ModifiedTime = dto.PlatformModifiedTime,
                Title = dto.PlatformTitle,
                Content = dto.PlatformContent,
                Type = dto.PlatformType,
                Display = dto.PlatformDisplay
            };
        }

        public static void UpdateEntity(this Platform entity, PlatformsDTO dto)
        {
            entity.AccountUserAccount = dto.PlatformAccountUserAccount;
            entity.ModifiedTime = dto.PlatformModifiedTime;
            entity.Title = dto.PlatformTitle;
            entity.Content = dto.PlatformContent;
            entity.Type = dto.PlatformType;
            entity.Display = dto.PlatformDisplay;
        }

        public static PlatformResponse ToEntity(this PlatformsDTO.Response res)
        {
            return new PlatformResponse
            {
                Id = res.ResponseId,
                AccountUserAccount = res.ResponseAccountUserAccount,
                ModifiedTime = res.ResponseModifiedTime,
                Content = res.ResponseContent,
                Display = res.ResponseDisplay
            };
        }

        public static void UpdateEntity(this PlatformResponse entity, PlatformsDTO.Response res)
        {
            entity.Id = res.ResponseId;
            entity.AccountUserAccount = res.ResponseAccountUserAccount;
            entity.ModifiedTime = res.ResponseModifiedTime;
            entity.Content = res.ResponseContent;
            entity.Display = res.ResponseDisplay;
        }

    }
}
