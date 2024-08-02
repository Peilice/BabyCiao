using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

    public class CompetitionRecordDTO
{
        public int voteId { get; set; }
        public string voterAccount { get; set; }
        public int CompetitorId { get; set; }
    
    }


public class CompetitionRecord_createDTO
{
    public int CompetitionId { get; set; }
    public string voterAccount { get; set; }
    public int CompetitorId { get; set; }

}