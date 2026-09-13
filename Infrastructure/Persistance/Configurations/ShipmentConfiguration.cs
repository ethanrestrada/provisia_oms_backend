using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.ToTable("shipment");

            builder.HasKey(s => s.ShipmentId);

            builder.Property(s => s.ShipmentId)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.TrackingNumber)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(s => s.Status)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(s => s.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            builder.HasIndex(s => s.TrackingNumber)
                  .IsUnique()
                  .HasFilter("deleted_at IS NULL");

            builder.HasOne(s => s.Order)
                  .WithMany()
                  .HasForeignKey(s => s.OrderId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Driver)
                  .WithMany()
                  .HasForeignKey(s => s.DriverId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Vehicle)
                  .WithMany()
                  .HasForeignKey(s => s.VehicleId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
