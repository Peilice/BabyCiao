using System.ComponentModel.DataAnnotations;

namespace BabyCiao.Models.DTO
{
	public class GroupBuyDTO
	{
		[Display(Name = "商品編號")]
		public int Id { get; set; }

		[Display(Name = "編輯者")]
		public string UserAccount { get; set; } = null!;//發布者(管理員) AccountUserAccount

		[Display(Name = "商品名稱")]
		public string ProductName { get; set; } = null!;

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

        [Display(Name = "顯示狀態")]
		public bool Display { get; set; }//顯示控制


        [Display(Name = "參團編號")]
        public int JoinQuantity { get; set; }//目前參加團購數

        //////////////////////////
        [Display(Name = "參團編號")]
		public int JoinId { get; set; }//參加編號 AccountUserAccount

        public int JoinGroupId { get; set; }//參加編號 AccountUserAccount


        [Display(Name = "會員帳號")]
		public string JoinUserAccount { get; set; } = null!;

		[Display(Name = "團購數量")]
		public int Quantity { get; set; }//加購數量

        [Display(Name = "商品價錢")]
        public int Price { get; set; }//加購數量
        [Display(Name = "商品價錢")]
        public int OrderPrice { get; set; }//加購數量
        [Display(Name = "下單時間")]
		public DateTime JoinModifiedTime { get; set; }//下單時間

        [Display(Name = "下單時間")]
        public string ViewJoinModifiedTime { get; set; }//下單時間
        [Display(Name = "參加狀態")]
		public string JoinStatement { get; set; } = null!;//參加狀態
		public List<GroupBuyPhotoDTO>? photos { get; set; }

		public List<IFormFile> PhotoFiles { get; set; }	
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
