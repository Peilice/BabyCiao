namespace BabyCiaoAPI.DTO
{
    public class EBook_GetBabyInfos_DTO
    {
        //HealthInformation的欄位
        public int HealthInfosId { get; set; }

        public int IdContactBook { get; set; }

        public string MedicalHistory { get; set; } //= null!;

        public string AllergyHistory { get; set; } //= null!;

        public int Height { get; set; }

        public int Weight { get; set; }

        public int HeadCircumference { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Memo { get; set; } //= null!;

        public string Age { get; set; } //= null!;

        //Ebook的欄位
        public string ParentsIdUserAccount { get; set; }
        public string BabyName { get; set; }
        public int Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string BloodType { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactPhone1 { get; set; }
        public string EmergencyContactPhone2 { get; set; }
        public string BabyPhoto { get; set; }

    }
}
