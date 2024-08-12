using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class NannyRequirment
{
    public int Id { get; set; }

    public DateTime RequirementDate { get; set; }

    public string NannyAccountUserAccount { get; set; } = null!;

    public string PoliceCriminalRecordCertificate { get; set; } = null!;

    public string ChildCareCertificate { get; set; } = null!;

    public string NationalIdentificationCard { get; set; } = null!;

    public string AddressesOfAgencies { get; set; } = null!;

    public DateOnly ValidPeriodsOfCertificates { get; set; }

    public int Statement { get; set; }

    public virtual UserAccount NannyAccountUserAccountNavigation { get; set; } = null!;
}
