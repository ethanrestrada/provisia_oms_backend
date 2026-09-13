using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payment");

            builder.HasKey(e => e.PaymentId);

            builder.Property(e => e.PaymentId)
                  .HasDefaultValueSql("gen_random_uuid()")
                  .ValueGeneratedOnAdd();

            builder.Property(e => e.Amount)
                  .IsRequired()
                  .HasPrecision(14, 2);

            builder.HasIndex(e => e.PaymentNumber).IsUnique();

            builder.HasOne(e => e.Invoice)
                  .WithMany(e => e.Payments)
                  .HasForeignKey(e => e.InvoiceId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.PaymentMethod)
                  .WithMany()
                  .HasForeignKey(e => e.PaymentMethodId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
