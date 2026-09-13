namespace Domain.Entities
{
    public class OrderStatusHistory
    {
        public int OrderStatusHistoryId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset ChangedAt { get; set; }
        public string? Notes { get; set; }
        public Guid OrderId { get; set; }

        // Navigation properties
        public Order Order { get; set; } = new Order();
    }
}
