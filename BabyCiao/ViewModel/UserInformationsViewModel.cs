using BabyCiao.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyCiao.ViewModel
{
    public class UserInformationsViewModel
    {

        public int UserId { get; set; }
        [Display(Name = "使用者")]

        public string AccountUser { get; set; } = null!;
        [Display(Name = "帳號")]

        public string UserFirstName { get; set; } = null!;
        [Display(Name = "名字")]

        public string UserLastName { get; set; } = null!;
        [Display(Name = "姓氏")]

        public string Phone { get; set; } = null!;
        [Display(Name = "電話")]

        public string Address { get; set; } = null!;
        [Display(Name = "地址")]

        public int Gender { get; set; }
        [Display(Name = "性別")]

        public string Email { get; set; } = null!;
        [Display(Name = "電子郵件")]

        public DateOnly Birthday { get; set; }
        [Display(Name = "生日")]

        public required List<UserInformation> UserInformations { get; set; }
        private static readonly Dictionary<int, string> GenderDictionary = new Dictionary<int, string>
            {
              { 1, "男" },
              { 2, "女" },
            };
        public string? searchString { get; set; }

    }

}
