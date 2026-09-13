namespace Domain.Entities
{
    public class Shipment
    {
        public Guid ShipmentId { get; set; } = Guid.CreateVersion7();
        public string TrackingNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid OrderId { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }

        // Navigation properties
        public Order Order { get; set; } = new Order();
        public Driver Driver { get; set; } = new Driver();
        public Vehicle Vehicle { get; set; } = new Vehicle();
    }
}
