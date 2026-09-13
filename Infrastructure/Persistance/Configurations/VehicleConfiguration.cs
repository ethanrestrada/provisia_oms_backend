using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicle");

            builder.HasKey(v => v.VehicleId);

            builder.Property(v => v.VehicleId)
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();

            builder.Property(v => v.PlateNumber)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(v => v.VehicleType)
                  .HasMaxLength(20)
                  .IsRequired();

            builder.Property(v => v.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .ValueGeneratedOnAdd();

            builder.Property(v => v.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            builder.HasIndex(v => v.PlateNumber)
                  .IsUnique()
                  .HasFilter("deleted_at IS NULL");
        }
    }
}
