using Azure;
using System.ComponentModel.DataAnnotations;

namespace BabyCiao.Models.DTO
{
    public class PlatformsDTO
    {
        public int PlatformId { get; set; }

        [Display(Name = "發佈者帳號")]
        public string PlatformAccountUserAccount { get; set; }

        [Display(Name = "修改時間")]
        public DateOnly PlatformModifiedTime { get; set; }

        [Display(Name = "文章標題")]
        public string PlatformTitle { get; set; }

        [Display(Name = "內文")]
        public string PlatformContent { get; set; }

        [Display(Name = "文章分類")]
        public string PlatformType { get; set; }

        [Display(Name = "顯示控制")]
        public bool PlatformDisplay { get; set; }
        public IEnumerable<Response>? Responses { get; set; }


        public class Response
        {

            public int ResponseId { get; set; }

            [Display(Name = "留言回應時間")]
            public DateTime ResponseModifiedTime { get; set; }

            [Display(Name = "留言回應")]
            public string ResponseContent { get; set; }

            [Display(Name = "顯示控制")]
            public bool ResponseDisplay { get; set; }

            [Display(Name = "回應人")]
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
