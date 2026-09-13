using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class ShipmentUpdateConfiguration : IEntityTypeConfiguration<ShipmentUpdate>
    {
        public void Configure(EntityTypeBuilder<ShipmentUpdate> builder)
        {
            builder.ToTable("shipment_update");

            builder.HasKey(e => e.ShipmentUpdateId);

            builder.Property(e => e.Location)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(e => e.Timestamp)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.HasOne(e => e.Shipment)
                  .WithMany()
                  .HasForeignKey(e => e.ShipmentId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
