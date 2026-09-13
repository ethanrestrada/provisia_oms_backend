namespace Domain.Entities
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; } = Guid.CreateVersion7();
        public string PlateNumber { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public bool HasRefrigeration { get; set; } = false;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
