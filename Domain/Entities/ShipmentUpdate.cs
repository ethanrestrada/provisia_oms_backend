namespace Domain.Entities
{
    public class ShipmentUpdate
    {
        public int ShipmentUpdateId { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTimeOffset Timestamp { get; set; }
        public Guid ShipmentId { get; set; }

        // Navigation properties
        public Shipment Shipment { get; set; } = new Shipment();
    }
}
