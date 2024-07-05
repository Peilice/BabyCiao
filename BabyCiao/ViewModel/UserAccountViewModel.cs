using System.ComponentModel.DataAnnotations;

namespace BabyCiao.ViewModels
{
    public class UserAccountViewModel
    {
        [Required(ErrorMessage = "請輸入用戶名")]
        public string Account { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
