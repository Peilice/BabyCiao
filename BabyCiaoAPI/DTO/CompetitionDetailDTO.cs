using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

public class CompetitionDetailDTO
{
    public int Id { get; set; }
    public string CompetitionName { get; set; }

    public DateOnly StartTime { get; set; }

    public DateOnly EndTime { get; set; }

    public string Content { get; set; }

    public string Statement { get; set; }

    //活動參賽者
    public string AccountUserAccount { get; set; }
    public string CompetitorPhoto { get; set; }

    public string CompetitorContent { get; set; }

    public string? CompetitionPhotos { get; set; }

    public IFormFile CompetitionPhoto { get; set; }
    public int CompetitionDetailId { get; set; }

    //個別選手得票數
    public int number {  get; set; }

    //活動總票數
    public int allnumber {  get; set; }
    
}


public class CompetitionDetail_createDTO
{
    public string AccountUserAccount { get; set; }
    public string? CompetitionPhotos { get; set; }
    public int CompetitionId { get; set; }
    public string Content { get; set; }


}