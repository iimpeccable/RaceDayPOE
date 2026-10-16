namespace RaceDay.API.Models
{
    public class Event
    {
        public int EventId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Location { get; set; } = string.Empty;

        public decimal Distance { get; set; }

        public string EventType { get; set; } = string.Empty;

        public int OrganizerId { get; set; }

        public User? Organizer { get; set; }
    }
}