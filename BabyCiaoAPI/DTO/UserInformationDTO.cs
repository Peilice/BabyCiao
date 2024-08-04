namespace BabyCiaoAPI.DTO
{
    public class UserInformationDTO
    {
        public int UserinfoId { get; set; }

        public string AccountUser { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string? UserPhoto { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public int Gender { get; set; }

        public string Email { get; set; }

        public string? Nickname { get; set; }

        public DateOnly Birthday { get; set; }

        public DateTime CreateddDate { get; set; }

        public DateTime ModiifiedDate { get; set; }

    }
}
