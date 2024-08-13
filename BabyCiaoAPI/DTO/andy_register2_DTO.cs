namespace BabyCiaoAPI.DTO
{
    public class andy_register2_DTO
    {
        public string AccountUser { get; set; } //= null!;
        public string Password { get; set; } //= null!;

        public string UserFirstName { get; set; } //= null!;

        public string UserLastName { get; set; } //= null!;


        public string Phone { get; set; } //= null!;

        public string Address { get; set; } //= null!;

        public int Gender { get; set; }

        public string Email { get; set; } //= null!;

        public string? Nickname { get; set; }

        public DateOnly Birthday { get; set; }
    }
}
