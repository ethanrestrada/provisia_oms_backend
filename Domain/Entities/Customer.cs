namespace Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; set; } = Guid.CreateVersion7();
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty;
        public string CustomerTaxId { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }

        // Navigation properties
        public User User { get; set; } = new User();
        public Company Company { get; set; } = new Company();
    }
}
