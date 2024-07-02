using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class UserAccount
{
    public int UserId { get; set; }

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Permissions { get; set; }

    public bool Vip { get; set; }

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<AuthGroup> AuthGroups { get; set; } = new List<AuthGroup>();

    public virtual ICollection<BabyResume> BabyResumes { get; set; } = new List<BabyResume>();

    public virtual ICollection<ContactBook> ContactBooks { get; set; } = new List<ContactBook>();

    public virtual ICollection<Contract> ContractAccountUserAccountNavigations { get; set; } = new List<Contract>();

    public virtual ICollection<Contract> ContractNannyAccountUserAccountNavigations { get; set; } = new List<Contract>();

    public virtual ICollection<Evaluate> EvaluateAppraiseeUserAccountNavigations { get; set; } = new List<Evaluate>();

    public virtual ICollection<Evaluate> EvaluateEvaluatorUserAccountNavigations { get; set; } = new List<Evaluate>();

    public virtual ICollection<ExchangeOrder> ExchangeOrders { get; set; } = new List<ExchangeOrder>();

    public virtual ICollection<GroupBuyingDetail> GroupBuyingDetails { get; set; } = new List<GroupBuyingDetail>();

    public virtual ICollection<GroupBuying> GroupBuyings { get; set; } = new List<GroupBuying>();

    public virtual ICollection<NannyRequirment> NannyRequirments { get; set; } = new List<NannyRequirment>();

    public virtual ICollection<NannyResume> NannyResumes { get; set; } = new List<NannyResume>();

    public virtual ICollection<OnlineCompetition> OnlineCompetitions { get; set; } = new List<OnlineCompetition>();

    public virtual ICollection<Platform> Platforms { get; set; } = new List<Platform>();

    public virtual ICollection<SecondHandSupply> SecondHandSupplies { get; set; } = new List<SecondHandSupply>();

    public virtual ICollection<UserInformation> UserInformations { get; set; } = new List<UserInformation>();

    public virtual ICollection<Vip> Vips { get; set; } = new List<Vip>();
}
