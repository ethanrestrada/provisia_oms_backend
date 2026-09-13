namespace Domain.Entities
{
    public class Company
    {
        public Guid CompanyId { get; set; } = Guid.CreateVersion7();
        public string Name { get; set; } = string.Empty;
        public string TaxId { get; set; } = string.Empty;
        public string LegalAddress { get; set; } = string.Empty;
        public decimal CreditLimit { get; set; } = 0.0M;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
