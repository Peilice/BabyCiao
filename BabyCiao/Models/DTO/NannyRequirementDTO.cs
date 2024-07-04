namespace BabyCiao.Models.DTO
{
    public class NannyRequirementDTO
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

        public IFormFile photo1 { get; set; }//保母證
        public IFormFile photo2 { get; set; }//身分證
        public IFormFile photo3 { get; set; }//良民證
    }
}
