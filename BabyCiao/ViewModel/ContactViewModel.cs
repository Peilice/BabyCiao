using System.ComponentModel.DataAnnotations;

namespace BabyCiao.ViewModel
{
    public class ContactViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "姓名欄位未填寫")]
        [StringLength(maximumLength:20, MinimumLength= 2, ErrorMessage = "姓名長度不正確")]

        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Email欄位未填寫")]
        [EmailAddress(ErrorMessage = "格式錯誤")]
        public string? Email { get; set; }
        
        
        [Required(ErrorMessage = "電話欄位未填寫")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "主旨未填寫")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "內容未填寫")]
        public string? content { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            {
                yield return new ValidationResult(
                    "電子郵件和電話號碼必須至少填寫一個欄位",
                    new string[] { "Email", "Phone" });
            }
        }
        }
    }

