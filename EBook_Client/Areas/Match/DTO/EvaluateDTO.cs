using BabyCiao.Models;

namespace BabyCiaoAPI.Models;

public partial class EvaluateDTO
{
    public int Id { get; set; }

    public string EvaluatorUserAccount { get; set; } = null!;

    public string AppraiseeUserAccount { get; set; } = null!;

    public DateOnly EvaluateTime { get; set; }

    public int Score { get; set; }

    public string? Memo { get; set; }

    public bool Display { get; set; }

    public virtual UserAccount AppraiseeUserAccountNavigation { get; set; } = null!;

    public virtual UserAccount EvaluatorUserAccountNavigation { get; set; } = null!;
}
