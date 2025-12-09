namespace SampleApiService
{
    public class SmsEvent
    {
        public string CustomerId { get; init; }
        public string FullName { get; init; } = null!;
        public string MobileNumber { get; init; } = null!;
        public DateTime RegisteredAt { get; init; }
    }
}
