using System;
using System.Collections.Generic;

namespace BabyCiao.Models;

public partial class NannyResume
{
    public int Id { get; set; }

    public string NannyAccountUserAccount { get; set; } = null!;

    public string City { get; set; } = null!;

    public string District { get; set; } = null!;

    public string? Introduction { get; set; }

    public string TypeOfDaycare { get; set; } = null!;

    public string ServiceItems { get; set; } = null!;

    public bool QuasiPublicChildcare { get; set; }

    public int ChildcareAvailableUnder2 { get; set; }

    public int ChildcareAvailableOver2 { get; set; }

    public string Language { get; set; } = null!;

    public string ServiceCenter { get; set; } = null!;

    public string? ProfessionalPortrait { get; set; }

    public string? InternalPhoto1 { get; set; }

    public string? InternalPhoto2 { get; set; }

    public string? InternalPhoto3 { get; set; }

    public string? InternalPhoto4 { get; set; }

    public string? InternalPhoto5 { get; set; }

    public bool DisplayControl { get; set; }

    public virtual UserAccount NannyAccountUserAccountNavigation { get; set; } = null!;
}
