namespace RaceDay.API.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public int EventId { get; set; }

        public Event? Event { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string Status { get; set; } = "Pending";
    }
}