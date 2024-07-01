using System.ComponentModel.DataAnnotations;

namespace BabyCiao.ViewModel
{
    public class ContactViewModel
    {
        [Required]

        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

        public string context { get; set; }
    }
}
