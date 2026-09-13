namespace Domain.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; } = Guid.CreateVersion7();
        public string Name { get; set; } = string.Empty;
        public Decimal Price { get; set; } = 0.0M;
        public int Stock { get; set; } = 0;
        public string Sku { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public int CategoryId { get; set; }

        // Navigation properties
        public ProductCategory Category { get; set; } = new ProductCategory();
    }
}
