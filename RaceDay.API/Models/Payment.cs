namespace RaceDay.API.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int RegistrationId { get; set; }

        public Registration? Registration { get; set; }

        public decimal Amount { get; set; }

        public string Method { get; set; } = string.Empty;
    }
}