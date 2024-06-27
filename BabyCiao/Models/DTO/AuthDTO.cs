namespace BabyCiao.Models.DTO
{
    public class AuthDTO
	{
		public int GroupCode { get; set; }// 來自 PermissionGroup
		public string GroupDescription { get; set; }// 來自 PermissionGroup

		public string ModifiedPersonUserAccount { get; set; } = null!;// 來自 PermissionGroup

		public DateTime ModifiedDate { get; set; }// 來自 PermissionGroup

		//public string FunctionCodeSystemFunction { get; set; } = null!;// 來自 FunctionSetting

		//          public DateTime ModifiedDateFunctionSetting { get; set; }// 來自 FunctionSetting

		public IEnumerable<FunctionSettingDTO> settings { get; set; }// 來自 FunctionSetting



            public DateTime ModifiedDatePermissionGroup { get; set; }//來自PermissionGroup

        
    }
    public class FunctionSettingDTO {

		public int FunctionCode { get; set; }//來自SystemFunction

		public string FunctionName { get; set; } = null!;//來自SystemFunction

		public int GroupCode { get; set; }//來自PermissionGroup
	}
}
