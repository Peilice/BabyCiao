using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class NannyRequirment
{
    public int Id { get; set; }

    public DateTime RequirementDate { get; set; }

    public string NannyAccountUserAccount { get; set; } = null!;

    public string PoliceCriminalRecordCertificate { get; set; } = null!;

    public string ChildCareCertificate { get; set; } = null!;

    public string NationalIdentificationCard { get; set; } = null!;

    public DateOnly AddressesOfAgencies { get; set; }

    public DateOnly ValidPeriodsOfCertificates { get; set; }

    public string Statement { get; set; } = null!;

    public virtual UserAccount NannyAccountUserAccountNavigation { get; set; } = null!;
}
