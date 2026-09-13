namespace Domain.Entities
{
    public class Driver
    {
        public Guid DriverId { get; set; } = Guid.CreateVersion7();
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public Guid UserId { get; set; }

        // Navigation properties
        public User User { get; set; } = new User();
    }
}
