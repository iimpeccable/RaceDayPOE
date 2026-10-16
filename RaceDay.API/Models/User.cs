namespace RaceDay.API.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Contact { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public Role? Role { get; set; }
    }
}
