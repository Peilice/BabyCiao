using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

public class PlatformDTO
{
    public int ArticleID { get; set; }
    public string PostAccount { get; set; }
    public string PostTitle { get; set; }
    public string PostContent { get; set; }
    public string PostType {  get; set; }
    public DateOnly PostModifiedTime { get; set; }
    public int ResponseCount { get; set; }

}

public class Platform_createDTO
{
    public string PostAccount { get; set; }
    public string PostTitle { get; set; }
    public string PostContent { get; set; }
    public string PostType { get; set; }

}

