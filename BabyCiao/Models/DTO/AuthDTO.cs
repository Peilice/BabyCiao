namespace BabyCiao.Models.DTO
{
    public class AuthDTO
    {

            public int GroupCodePermissionGroup { get; set; }// 來自 FunctionSetting

            public string FunctionCodeSystemFunction { get; set; } = null!;// 來自 FunctionSetting

            public DateTime ModifiedDateFunctionSetting { get; set; }// 來自 FunctionSetting



            public string FunctionCode { get; set; } = null!;//來自SystemFunction

            public string FunctionName { get; set; } = null!;//來自SystemFunction



            public int GroupCode { get; set; }//來自PermissionGroup


            public string GroupDescription { get; set; } = null!;//來自PermissionGroup


            public string ModifiedPersonUserAccount { get; set; } = null!;

            public DateTime ModifiedDatePermissionGroup { get; set; }//來自PermissionGroup



        
    }
}
