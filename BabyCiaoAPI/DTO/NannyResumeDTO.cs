public partial class NannyResumeDTO
{
    public int Id { get; set; }

    public string NannyAccountUserAccount { get; set; } = null!;

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

    public bool? DisplayControl { get; set; }

}


public partial class NRPDTO
{
    public int Id { get; set; }

    public int IdNannyResume { get; set; }

    public string PhotoName { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }
}
