namespace RaceDay.API.DTOs
{
    public class RegisterForEventDto
    {
        public int CategoryId { get; set; }
    }

    public class RegistrationResponseDto
    {
        public int RegistrationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }

    public class CreateResultDto
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public TimeSpan Time { get; set; }
        public int Position { get; set; }
    }

    public class ResultResponseDto
    {
        public int ResultId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public TimeSpan Time { get; set; }
        public int Position { get; set; }
    }
}