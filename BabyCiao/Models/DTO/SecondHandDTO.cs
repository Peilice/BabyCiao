using System.ComponentModel.DataAnnotations;

namespace BabyCiao.Models.DTO
{
	public class SecondHandDTO
	{

		[Display(Name = "商品編號")]
		public int Id { get; set; }

		[Display(Name = "用戶")]
		public string AccountUserAccount { get; set; } = null!;

		[Display(Name = "商品名稱")]
		public string SuppliesName { get; set; } = null!;

		[Display(Name = "敘述")]
		public string SuppliesDescription { get; set; } = null!;

		[Display(Name = "商品數量")]
		public int StockQuantity { get; set; }

		[Display(Name = "編輯時間")]
		public DateTime ModifiedTime { get; set; }
        [Display(Name = "編輯時間")]
        public string ModifiedTimeString { get; set; }
        [Display(Name = "種類")]
		public string Type { get; set; } = null!;

		[Display(Name = "顯示")]
		public bool Display { get; set; }
		[Display(Name = "顯示")]
		public string DisplayString { get; set; }//顯示控制

		public List<SecondHandPhotoDTO>? Photos { get; set; }

		[Display(Name = "商品照片")]
		public List<IFormFile> PhotoFiles { get; set; }
	}

	public class SecondHandPhotoDTO
	{
		public int Id { get; set; }

		public int IdSecondHandSupplies { get; set; }

		public string PhotoName { get; set; } = null!;

		public string ModifiedTime { get; set; } = null!;
	}
}
