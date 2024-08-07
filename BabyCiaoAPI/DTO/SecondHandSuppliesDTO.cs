using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.DTO
{
	public class SecondHandSuppliesDTO
	{
		[Display(Name = "商品編號")]
		public int Id { get; set; }

		[Display(Name = "上傳用戶")]
		public string AccountUserAccount { get; set; } = null!;

		[Display(Name = "商品名稱")]
		public string SuppliesName { get; set; } = null!;

		[Display(Name = "商品敘述")]
		public string SuppliesDescription { get; set; } = null!;

		[Display(Name = "庫存")]
		public int StockQuantity { get; set; }


		[Display(Name = "上傳時間")]
		public string? ModifiedTimeView { get; set; }

		[Display(Name = "類別")]
		public string Type { get; set; } = null!;

		

		[Display(Name = "照片")]
		public string? Photo { get; set; }

	}

	public class SecondHandFilterDTO
	{

		[Display(Name = "商品編號")]
		public int Id { get; set; }

		[Display(Name = "上傳用戶")]
		public string? AccountUserAccount { get; set; }

		[Display(Name = "商品名稱")]
		public string SuppliesName { get; set; } = null!;

		[Display(Name = "商品敘述")]
		public string SuppliesDescription { get; set; } = null!;


		[Display(Name = "類別")]
		public string? Type { get; set; }

		[Display(Name = "照片")]
		public string? Photo { get; set; }

	}
	public partial class SecondHandCreate
	{
		public int Id { get; set; }

		public string AccountUserAccount { get; set; } = null!;

		public string SuppliesName { get; set; } = null!;

		public string SuppliesDescription { get; set; } = null!;

		public int StockQuantity { get; set; }

		public DateTime ModifiedTime { get; set; }

		public string Type { get; set; } = null!;

		public bool Display { get; set; }
		 
		//public List<SuppliesPhotoDTO>? Photos { get; set; }
		//public List<IFormFile>? PhotoFiles { get; set; }//上傳檔案
	}
        public class SuppliesPhotoDTO
	{
		public int Id { get; set; }

		public int IdSecondHandSupplies { get; set; }

		public string PhotoName { get; set; } = null!;

		public string ModifiedTime { get; set; } = null!;
	}

}
