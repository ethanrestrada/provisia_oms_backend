namespace Domain.Entities
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
