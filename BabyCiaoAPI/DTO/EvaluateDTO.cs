using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models
{
    public class UserEvaluationDTO
    {
        public string Nickname { get; set; } = null!;
        public string? UserPhoto { get; set; }
        public int Score { get; set; } // Score will be between 0-5
        public string? Memo { get; set; } // User's comment
    }
}



