using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class NannyRequirmentDTO
{
    public int Id { get; set; } 

    public DateOnly RequirementDate { get; set; } 

    public string NannyAccountUserAccount { get; set; } 
    public IFormFile PoliceCriminalRecordCertificate { get; set; }

    public string PoliceCriminalRecordCertificates { get; set; }
    public IFormFile ChildCareCertificate { get; set; }

    public string ChildCareCertificates { get; set; }
    public IFormFile NationalIdentificationCard { get; set; }

    public string NationalIdentificationCards { get; set; } 

    public string AddressesOfAgencies { get; set; } 

    public DateOnly ValidPeriodsOfCertificates { get; set; }

    public int Statement { get; set; }
    public string? Nickname { get; set; }


}

public partial class NannyRequirment_NEWDTO
{
    public int Id { get; set; }

    public DateOnly RequirementDate { get; set; }

    public string NannyAccountUserAccount { get; set; }

    public string PoliceCriminalRecordCertificate { get; set; }

    public string ChildCareCertificate { get; set; }

    public string NationalIdentificationCard { get; set; }

    public string AddressesOfAgencies { get; set; }

    public DateOnly ValidPeriodsOfCertificates { get; set; }
    public string? Nickname { get; set; }
    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string? Introduction { get; set; }

    public string TypeOfDaycare { get; set; } = null!;

    public int ServiceType { get; set; }

    public string ServiceItems { get; set; } = null!;

    public bool QuasiPublicChildcare { get; set; }

    public int ChildcareAvailableUnder2 { get; set; }

    public int ChildcareAvailableOver2 { get; set; }

    public string Language { get; set; } = null!;

    public string ServiceCenter { get; set; } = null!;

    public string ProfessionalPortrait { get; set; } = null!;


}
public partial class NannyRequirmentNEWDTO
{
    public string NannyAccountUserAccount { get; set; }

    public string PoliceCriminalRecordCertificate { get; set; }

    public string ChildCareCertificate { get; set; }

    public string NationalIdentificationCard { get; set; }

    public string AddressesOfAgencies { get; set; }

    public DateOnly ValidPeriodsOfCertificates { get; set; }

}