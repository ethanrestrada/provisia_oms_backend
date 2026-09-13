using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<OrderStatusHistory> OrderStatusHistories => Set<OrderStatusHistory>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<Shipment> Shipments => Set<Shipment>();
        public DbSet<ShipmentUpdate> ShipmentUpdates => Set<ShipmentUpdate>();
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
