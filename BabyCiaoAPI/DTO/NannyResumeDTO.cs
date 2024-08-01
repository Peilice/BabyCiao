namespace BabyCiaoAPI.DTO
{
    public class NannyResumeDTO
    {
    public int Id { get; set; }
        public string NannyAccountUserAccount { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    public string? Introduction { get; set; }
        public string TypeOfDaycare { get; set; }
        public string ServiceItems { get; set; }
    public bool QuasiPublicChildcare { get; set; }

    public int ChildcareAvailableUnder2 { get; set; }

    public int ChildcareAvailableOver2 { get; set; }
        public string Language { get; set; }
        public string ServiceCenter { get; set; }
    public string? ProfessionalPortrait { get; set; }

    public bool DisplayControl { get; set; }

        public string Photo { get; set; }
    }
}
