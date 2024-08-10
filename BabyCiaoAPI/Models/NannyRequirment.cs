using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class NannyRequirment
{
    public int Id { get; set; }

    public DateOnly RequirementDate { get; set; }

    public string NannyAccountUserAccount { get; set; } 

    public string PoliceCriminalRecordCertificate { get; set; }

    public string ChildCareCertificate { get; set; } 

    public string NationalIdentificationCard { get; set; } 

    public string AddressesOfAgencies { get; set; } 

    public DateOnly ValidPeriodsOfCertificates { get; set; }

    public int Statement { get; set; }


    public virtual UserAccount NannyAccountUserAccountNavigation { get; set; } = null!;
}
