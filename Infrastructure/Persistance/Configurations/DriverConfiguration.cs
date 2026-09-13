using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("driver");

            builder.HasKey(d => d.DriverId);

            builder.Property(d => d.DriverId)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();

            builder.Property(d => d.Name)
                  .IsRequired()
                  .HasMaxLength(255);

            builder.Property(d => d.Phone)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(d => d.LicenseNumber)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(d => d.CreatedAt)
                  .IsRequired()
                  .HasDefaultValueSql("CURRENT_TIMESTAMP")
                  .ValueGeneratedOnAdd();

            builder.Property(d => d.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            builder.HasIndex(d => d.LicenseNumber)
                .IsUnique()
                .HasFilter("deleted_at IS NULL");

            builder.HasOne(d => d.User)
                  .WithMany()
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
