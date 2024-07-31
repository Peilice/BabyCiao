using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.DTO
{
    public class GBDTO
    {

        [Display(Name = "商品編號")]
        public int Id { get; set; }

        [Display(Name = "編輯者")]
        public string UserAccount { get; set; } = null!;//發布者(管理員) AccountUserAccount

        [Display(Name = "商品名稱")]
        public string ProductName { get; set; } = null!;

        [Display(Name = "商品種類")]
        public string? ProductType { get; set; }
        [Display(Name = "商品描述")]
        public string ProductDescription { get; set; } = null!;

        [Display(Name = "成團數量")]
        public int TargetCount { get; set; }//成團數

        [Display(Name = "成團狀態")]
        public string Statement { get; set; } = null!;//階段

        [Display(Name = "建立時間")]
        public DateTime ModifiedTime { get; set; }//建立時間
        [Display(Name = "建立日期")]
        public string ModifiedTimeView { get; set; }//建立時間

        [Display(Name = "顯示")]
        public bool Display { get; set; }//顯示控制
        [Display(Name = "顯示")]
        public string DisplayString { get; set; }//顯示控制


        [Display(Name = "目前參加團購數")]
        public int JoinQuantity { get; set; }//目前參加團購數

        public string? photoUrl { get; set; }
        public List<GroupBuyPhotoDTO>? Photos { get; set; }

        [Display(Name = "商品照片")]
        public List<IFormFile>? PhotoFiles { get; set; }
    }


    public class GBFilterDTO
    {

        [Display(Name = "商品編號")]
        public int Id { get; set; }

        [Display(Name = "商品名稱")]
        public string ProductName { get; set; } = null!;

        [Display(Name = "商品描述")]
        public string ProductDescription { get; set; } = null!;

        public string? photoUrl { get; set; }

    }
    public class GroupBuyPhotoDTO
    {
        [Display(Name = "照片編號")]
        public int Id { get; set; }

        [Display(Name = "商品編號")]
        public int IdGroupBuying { get; set; }

        [Display(Name = "照片路徑")]
        public string PhotoName { get; set; } = null!;

        [Display(Name = "上傳時間")]
        public DateTime ModifiedTime { get; set; }
    }
}
