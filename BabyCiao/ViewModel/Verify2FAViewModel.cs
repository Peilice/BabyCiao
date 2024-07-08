using System.ComponentModel.DataAnnotations;

namespace BabyCiao.ViewModels
{
    public class Verify2FAViewModel
    {
        [Required(ErrorMessage = "請輸入驗證碼")]
        public string TwoFactorCode { get; set; }
    }
}
