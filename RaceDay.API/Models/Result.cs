namespace RaceDay.API.Models
{
    public class Result
    {
        public int ResultId { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public int EventId { get; set; }

        public Event? Event { get; set; }

        public TimeSpan Time { get; set; }

        public int Position { get; set; }
    }
}