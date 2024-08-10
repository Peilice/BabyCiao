using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

public class Favorite_createDTO
{
    public int ArticleID { get; set; }
    public string favoriteAccount { get; set; }

}

