using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BabyCiao.Models.DTO
{
    public partial class NannyResumeDTO
    {

        public int id { get; set; } // Corrected property name to match common naming conventions
        [Display(Name = "帳號")]
        public string NannyAccountUserAccount { get; set; } = null!;
        [Display(Name = "城市")]
        public string City { get; set; } = null!;
        [Display(Name = "區域")]
        public string District { get; set; } = null!;
        [Display(Name = "自我介紹")]
        public string? Introduction { get; set; }
        [Display(Name = "托育時段")]
        public string TypeOfDaycare { get; set; } = null!;
        [Display(Name = "服務項目")]
        public string ServiceItems { get; set; } = null!;
        [Display(Name = "公共托育")]
        public bool QuasiPublicChildcare { get; set; }
        [Display(Name = "可收托2歲以下")]
        public int ChildcareAvailableUnder2 { get; set; }
        [Display(Name = "可收托2歲以上")]
        public int ChildcareAvailableOver2 { get; set; }
        [Display(Name = "語言")]
        public string Language { get; set; } = null!;
        [Display(Name = "收托地點")]
        public string ServiceCenter { get; set; } = null!;
        [Display(Name = "個人照片")]
        public string? ProfessionalPortrait { get; set; }
        [Display(Name = "顯示")]
        public bool DisplayControl { get; set; }

        public string? PhotoUrl { get; set; }
        public List<NannyResumePhotoDTO> Photos { get; set; } = new List<NannyResumePhotoDTO>();

        [Display(Name = "環境照片")]
        public List<IFormFile> PhotoFiles { get; set; } = new List<IFormFile>();

        public List<string> PhotoName { get; set; } = new List<string>();

        public virtual ICollection<NannyResumePhoto> NannyResumePhotos { get; set; } = new List<NannyResumePhoto>();

        public virtual UserAccount NannyAccountUserAccountNavigation { get; set; } = null!;
        public object JoinQuantity { get; internal set; }
        public string? Nickname { get; internal set; }
    }

    public partial class NannyResumePhotoDTO
    {
        [Display(Name = "環境編號")]
        public int Id { get; set; }
        [Display(Name = "照片編號")]
        public int IdNannyResume { get; set; }
        [Display(Name = "照片路徑")]
        public string PhotoName { get; set; } = null!;
        [Display(Name = "上傳時間")]
        public DateTime ModifiedTime { get; set; }
        [Display(Name = "環境照片")]
        public List<IFormFile> PhotoFiles { get; set; } = new List<IFormFile>();
    }

    public partial class UserInformationDTO
    {
        public int UserinfoId { get; set; }

        public string AccountUser { get; set; } = null!;

        public string UserFirstName { get; set; } = null!;

        public string UserLastName { get; set; } = null!;

        public string? UserPhoto { get; set; }

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int Gender { get; set; }

        public string Email { get; set; } = null!;
        [Display(Name = "暱稱")]

        public string? Nickname { get; set; }

        public DateOnly Birthday { get; set; }

        public DateTime CreateddDate { get; set; }

        public DateTime ModiifiedDate { get; set; }

        public virtual UserAccount AccountUserNavigation { get; set; } = null!;
    }

}
