namespace Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; set; } = Guid.CreateVersion7();
        public string PaymentNumber { get; set; } = string.Empty;
        public DateTimeOffset PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public Guid InvoiceId { get; set; }

        // Navigation properties
        public PaymentMethod PaymentMethod { get; set; } = new PaymentMethod();
        public Invoice Invoice { get; set; } = new Invoice();
    }
}
