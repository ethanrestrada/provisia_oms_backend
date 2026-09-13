namespace Domain.Entities
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; } = Guid.CreateVersion7();
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTimeOffset IssueDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid OrderId { get; set; }
        public decimal Total { get; set; } = 0.0M;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        // Navigation properties
        public Order Order { get; set; } = new Order();
        public ICollection<Payment> Payments { get; set; } = [];
    }
}
