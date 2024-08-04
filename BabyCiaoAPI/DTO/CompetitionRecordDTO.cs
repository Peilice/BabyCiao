using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

public class CompetitionRecordDTO
{
    public int voteId { get; set; }
    public string voterAccount { get; set; }
    public string CompetitorName { get; set; }
    public string CompetitionName { get; set; }
    public string Statement { get; set; }
    public string CompetitorPhoto {  get; set; }
    public string Content { get; set; }
    
}


public class CompetitionRecord_createDTO
{
    public int CompetitionId { get; set; }
    public string voterAccount { get; set; }
    public int CompetitorId { get; set; }

}