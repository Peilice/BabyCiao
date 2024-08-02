using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.DTO
{
    public class GBOrderDTO
    {
        [Display(Name = "參團編號")]
        public int JoinId { get; set; }//參加編號 

        public int JoinGroupId { get; set; }//參加團號 


        [Display(Name = "會員帳號")]
        public string JoinUserAccount { get; set; } = null!;




        [Display(Name = "團購數量")]
        public int Quantity { get; set; }//加購數量
        [Display(Name = "訂購數/成團數")]
        public int Percent { get; set; }//加購數量

        [Display(Name = "商品價錢")]
        public int Price { get; set; }//$$
        [Display(Name = "商品價錢")]
        public int OrderPrice { get; set; }//訂單價格
        [Display(Name = "下單時間")]
        public DateTime JoinModifiedTime { get; set; }//下單時間

        [Display(Name = "下單時間")]
        public string ViewJoinModifiedTime { get; set; }//下單時間
        [Display(Name = "參加狀態")]
        public string JoinStatement { get; set; } = null!;//參加狀態
        [Display(Name = "地址")]
        public string Address { get; set; } = null!;//參加狀態
        public string? photoUrl { get; set; }
        public List<GroupBuyPhotoDTO>? Photos { get; set; }

        //[Display(Name = "商品規格")]
        //public List<GroupBuyFormateDTO>? ProductFormats { get; set; }
        [Display(Name = "訂單規格")]
        public List<GroupBuyOrderFormatDTO>? OrderFormat { get; set; }
        [Display(Name = "商品照片")]
        public List<IFormFile> PhotoFiles { get; set; }
    }
    //public partial class GroupBuyFormateDTO
    //{
    //    [Display(Name = "規格編號")]
    //    public int Id { get; set; }
    //    [Display(Name = "商品編號")]
    //    public int IdGroupBuying { get; set; }
    //    [Display(Name = "規格名稱")]
    //    public string FormatType { get; set; } = null!;
    //    [Display(Name = "規格項目")]
    //    public string FormatName { get; set; } = null!;


    //}
    public class GroupBuyOrderFormatDTO
    {
        public int Id { get; set; }

        public int GroupBuyingDetailId { get; set; }

        public int FormatId { get; set; }

        [Display(Name = "規格名稱")]
        public string FormatType { get; set; } = null!;

        [Display(Name = "規格項目")]
        public string FormatName { get; set; } = null!;
    }
}
