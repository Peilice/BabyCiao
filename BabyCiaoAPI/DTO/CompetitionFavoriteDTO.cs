using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

public class CompetitionFavoriteDTO
{
    public int FavoriteId { get; set; }
    public string CompetitionName { get; set; }
    public int CompetitionId {  get; set; }
    public string CompetitionContent { get; set; }
    public DateOnly StartTime { get; set; }
    public DateOnly EndTime { get; set; }
    public string Statement { get; set; }
    public string CompetitionPhoto {  get; set; }

}

public class CompetitionFavorite_createDTO
{

    public string myAccount { get; set; }

    public int CompetitionId { get; set; }

}
