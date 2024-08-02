using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

    public class CompetitionDetail_createDTO
{
        public string AccountUserAccount { get; set; }
        public string? CompetitionPhotos { get; set; }
        public int CompetitionId { get; set; }
        public string Content { get; set; }


    }
