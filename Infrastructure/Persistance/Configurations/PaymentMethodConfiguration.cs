using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("payment_method");

            builder.HasKey(e => e.PaymentMethodId);

            builder.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(e => e.Description)
                  .HasMaxLength(20);

            builder.HasIndex(e => e.Name)
                  .IsUnique();
        }
    }
}
