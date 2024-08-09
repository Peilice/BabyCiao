namespace BabyCiaoAPI.DTO
{
    public class Ebook_UpdateBabyInfos_DTO
    {
        
        public string? BloodType { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyContactPhone1 { get; set; }
        public string? EmergencyContactPhone2 { get; set; }
        public IFormFile? BabyPhoto { get; set; }

       
    }
}
