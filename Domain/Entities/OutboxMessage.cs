namespace Domain.Entities
{
    public class OutboxMessage
    {
        public int OutboxMessageId { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTimeOffset ProcessedAt { get; set; }
        public string? Error { get; set; }
    }
}
