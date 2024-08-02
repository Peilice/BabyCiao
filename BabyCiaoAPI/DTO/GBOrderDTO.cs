using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.DTO
{
    public class GBOrderDTO
    {

        public int Id { get; set; }//訂單編號
        public int GroupBuyingId { get; set; }//參加團號 


        [Display(Name = "會員帳號")]
        public string UserAccount { get; set; } = null!;
        
        [Display(Name = "地址")]
        public string Address { get; set; } = null!;

        public string? Note { get; set; }

        [Display(Name = "下單時間")]
        public DateTime JoinModifiedTime { get; set; }//下單時間

        [Display(Name = "參加狀態")]
        public string JoinStatement { get; set; } = null!;//參加狀態

        [Display(Name = "商品規格")]
        public List<GroupBuyOrderFormatDTO>? OrderFormats { get; set; }
       
    }

        public class GroupBuyOrderFormatDTO
    {

        public int OrderFormatId { get; set; }//訂單編號
        public int GroupBuyingDetailId { get; set; }//訂單編號

        public int? FormatId { get; set; }//訂單規格ID

        public int Quantity { get; set; }//此規格數量

    }
}
