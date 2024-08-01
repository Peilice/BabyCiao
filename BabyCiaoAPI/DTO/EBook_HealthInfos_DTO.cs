namespace BabyCiaoAPI.DTO
{
    public class EBook_HealthInfos_DTO
    {
        public int Id { get; set; }

        public int IdContactBook { get; set; }

        public string MedicalHistory { get; set; } //= null!;

        public string AllergyHistory { get; set; } //= null!;

        public int Height { get; set; }

        public int Weight { get; set; }

        public int HeadCircumference { get; set; }

        //public DateTime ModifiedDate { get; set; }

        public string Memo { get; set; } //= null!;
    }
}
