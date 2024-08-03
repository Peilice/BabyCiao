using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

    public class OnlineCompetitionsDTO
    {
        public int Id { get; set; }
        [Display(Name = "活動名稱")]
        public string CompetitionName { get; set; }

        [Display(Name = "修改人員")]
        public string AccountUserAccount { get; set; }

        [Display(Name = "活動開始時間")]
        public DateOnly StartTime { get; set; }

        [Display(Name = "活動結束時間")]
        public DateOnly EndTime { get; set; }

        [Display(Name = "活動介紹")]
        public string Content { get; set; }

        [Display(Name = "活動進行狀態")]
        public string Statement { get; set; }

        [Display(Name = "更新時間")]
        public DateOnly ModifiedTime { get; set; }

        //活動照片
        
        public string? CompetitionPhotoNames { get; set; }
        [Display(Name = "活動照片")]
        public IFormFile CompetitionPhotoName { get; set; }
        public int CompetitionPhotoId { get; set; }
        public DateTime CompetitionPhotoModifiedTime { get; set; }
        public int IdOnlineCompetition { get; set; }
        public string photoUrl {  get; set; }

    }

