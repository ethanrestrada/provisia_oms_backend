namespace Domain.Entities
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public string Action { get; set; } = string.Empty;
        public DateTimeOffset Timestamp { get; set; }
        public Guid UserId { get; set; }

        // Navigation properties
        public User User { get; set; } = new User();
    }
}
