using System.ComponentModel.DataAnnotations;


namespace BabyCiao.Models.DTO
{
    public class AuthDTO
	{
        [Display(Name = "群組編號")]
        public int GroupId { get; set; }// 來自 PermissionGroup

        [Display(Name = "群組名稱")]
        public string GroupDescription { get; set; }// 來自 PermissionGroup

        [Display(Name = "編輯者")]
        public string ModifiedPersonUserAccount { get; set; } = null!;// 來自 PermissionGroup

        [Display(Name = "更新日期")]
        public DateTime ModifiedDate { get; set; }// 來自 PermissionGroup

        [Display(Name = "更新日期")]
        public string? ModifiedDateStringOld { get; set; }//上次更新日期
        [Display(Name = "更新日期")]
        public string? ModifiedDateStringNew { get; set; }//此次更新日期
        [Display(Name = "功能權限")]
        public List<FunctionSettingDTO>? settings { get; set; }// 來自 FunctionSetting
            //=new List<FunctionSettingDTO>()





	}

    public class FunctionSettingDTO {
        public bool IsExsited { get; set; }

		public int GroupId { get; set; }//來自PermissionGroup
		public int FunctionId { get; set; }//來自SystemFunction

		public string? FunctionName { get; set; }//來自SystemFunction

	}
}
