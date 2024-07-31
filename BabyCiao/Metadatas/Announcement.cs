using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BabyCiao.Models
{
    [ModelMetadataType(typeof(AnnouncementMetadata))]
    public partial class Announcement
    {
       
    }

    public class AnnouncementMetadata
    {
        [Display(Name = "帳號")]
        public string AccountUserAccount { get; set; } = null!;
        [Display(Name = "建立時間")]
        public DateTime PublishTime { get; set; }
        [Display(Name = "標題")]
        public string Tittle { get; set; } = null!;
        [Display(Name = "文章內容")]
        public string Article { get; set; } = null!;
        [Display(Name = "來源名稱")]
        public string ReferenceName { get; set; } = null!;
        [Display(Name = "來源路徑")]
        public string ReferenceRoute { get; set; } = null!;
        [Display(Name = "類別")]
        public string Type { get; set; } = null!;
        [Display(Name = "顯示控制")]
        public bool Display { get; set; }
        [Display(Name = "創建者")]
        public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;
    }
}
