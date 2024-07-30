using BabyCiao.Models;

namespace BabyCiaoAPI.Models;

public partial class ContractDTO
{
    public int ContractId { get; set; }

    public string NannyAccountUserAccount { get; set; } = null!;

    public bool NannySignature { get; set; }

    public string AccountUserAccount { get; set; } = null!;

    public bool UserSignature { get; set; }

    public DateOnly ContractStartTime { get; set; }

    public DateOnly ContractFinishTime { get; set; }

    public string ContractFile { get; set; } = null!;

    public string Statement { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public DateTime BuiledTime { get; set; }

    public bool Display { get; set; }

    public virtual UserAccount AccountUserAccountNavigation { get; set; } = null!;

    public virtual UserAccount NannyAccountUserAccountNavigation { get; set; } = null!;
}
