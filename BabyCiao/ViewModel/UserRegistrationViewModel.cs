using System.ComponentModel.DataAnnotations;

namespace BabyCiao.ViewModels
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "請輸入用戶名")]
        public string Account { get; set; }

        [Required(ErrorMessage = "請輸入電子郵件地址")]
        [EmailAddress(ErrorMessage = "電子郵件地址無效")]
        public string Email { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "請同意條款和政策")]
        public bool Agree { get; set; }
    }
}
