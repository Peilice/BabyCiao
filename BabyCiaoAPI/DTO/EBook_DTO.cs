﻿namespace BabyCiaoAPI.DTO
{
    public class EBook_DTO
    {
        public int Id { get; set; }
        public string ParentsIdUserAccount { get; set; }
        public string BabyName { get; set; }
        public int Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string BloodType { get; set; }
        public string EmergencyContact { get; set; }
        public string EmergencyContactPhone1 { get; set; }
    }
}
