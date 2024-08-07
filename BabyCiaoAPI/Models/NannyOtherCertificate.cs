using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class NannyOtherCertificate
{
    public string NannyAccountUserAccount { get; set; } = null!;

    public string OtherCertificate { get; set; } = null!;

    public string OtherCertificateName { get; set; } = null!;

    public DateTime CreateddDate { get; set; }

    public DateTime ModiifiedDateDatetime { get; set; }

    public virtual UserAccount NannyAccountUserAccountNavigation { get; set; } = null!;
}
