namespace Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.CreateVersion7();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public int RoleId { get; set; }

        // Navigation properties
        public Role Role { get; set; } = new Role();
    }
}
