public class NannyResumeDetailDTO
{
    public int Id { get; set; }
    public string NannyAccountUserAccount { get; set; } = null!;
    public string? Nickname { get; set; }
    public string City { get; set; } = null!;
    public string District { get; set; } = null!;
    public string? Introduction { get; set; }
    public string TypeOfDaycare { get; set; } = null!;
    public string ServiceType { get; set; } = null!;
    public string ServiceItems { get; set; } = null!;
    public bool QuasiPublicChildcare { get; set; }
    public int ChildcareAvailableUnder2 { get; set; }
    public int ChildcareAvailableOver2 { get; set; }
    public string Language { get; set; } = null!;
    public string ServiceCenter { get; set; } = null!;
    public string ProfessionalPortrait { get; set; } = null!;
    public bool? DisplayControl { get; set; }
    public IEnumerable<EvaluateDTO> Evaluations { get; set; } = new List<EvaluateDTO>();
    public IEnumerable<InquireDTO> Inquiries { get; set; } = new List<InquireDTO>();
    public IEnumerable<NannyResumePhotoDTO> Photos { get; set; } = new List<NannyResumePhotoDTO>();
}

public class EvaluateDTO
{
    public int Id { get; set; }
    public string EvaluatorUserAccount { get; set; } = null!;
    public string AppraiseeUserAccount { get; set; } = null!;
    public DateOnly EvaluateTime { get; set; }
    public int Score { get; set; }
    public string? Memo { get; set; }
    public bool Display { get; set; }
}

public class InquireDTO
{
    public string UserAccountresponse { get; set; } = null!;
    public string UserAccountinquire { get; set; } = null!;
    public int Times { get; set; }
}

public class NannyResumePhotoDTO
{
    public int Id { get; set; }
    public int IdNannyResume { get; set; }
    public string PhotoName { get; set; } = null!;
    public DateTime ModifiedTime { get; set; }
}
