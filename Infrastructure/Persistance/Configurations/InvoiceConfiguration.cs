using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoice");

            builder.HasKey(i => i.InvoiceId);

            builder.Property(i => i.InvoiceId)
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();

            builder.Property(i => i.InvoiceNumber)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(i => i.Status)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(i => i.Total)
                .IsRequired()
                .HasPrecision(14, 2);

            builder.HasIndex(i => i.InvoiceNumber)
                  .IsUnique()
                  .HasFilter("deleted_at IS NULL");

            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            builder.HasOne(i => i.Order)
                  .WithMany()
                  .HasForeignKey(i => i.OrderId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
