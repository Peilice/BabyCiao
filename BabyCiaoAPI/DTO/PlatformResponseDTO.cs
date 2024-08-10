using BabyCiao.Models;
using System.ComponentModel.DataAnnotations;

namespace BabyCiaoAPI.Models;

public class PlatformResponseDTO
{
    //讀取文章
    public int ArticleID { get; set; }
    public string PostAccount { get; set; }
    public string PostTitle { get; set; }
    public string PostContent { get; set; }
    public string PostType { get; set; }
    public DateOnly PostModifiedTime { get; set; }

    //讀取回應
    public int ResponseID { get; set; }
    public string ResponseAccount { get; set; }
    public string ResponseContent { get; set; }
    public DateTime ResponseModifiedTime { get; set; }
    public string? ResponseModifiedTimeView { get; set; }

}

public class Response_createDTO
{
    public int ArticleID { get; set; }
    public string ResponseAccount { get; set; }
    public string ResponseContent { get; set; }
}

