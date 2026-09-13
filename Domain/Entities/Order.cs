namespace Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.CreateVersion7();
        public string OrderNumber { get; set; } = string.Empty;
        public DateTimeOffset OrderDate { get; set; }
        public decimal TotalAmount { get; set; } = 0.0M;
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public Guid UserId { get; set; }

        // Navigation properties
        public Customer Customer = new Customer();
        public User User = new User();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
