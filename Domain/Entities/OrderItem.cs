namespace Domain.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        // Navigation properties
        public Order Order { get; set; } = new Order();
        public Product Product { get; set; } = new Product();
    }
}
