using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<OrderStatusHistory>
    {
        public void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
        {
            builder.ToTable("order_status_history");

            builder.HasKey(osh => osh.OrderStatusHistoryId);
            
            builder.Property(osh => osh.Status)
                .HasMaxLength(20);

            builder.Property(osh => osh.ChangedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(osh => osh.Notes)
                .HasMaxLength(510);

            builder.HasOne(osh => osh.Order)
                .WithMany()
                .HasForeignKey(osh => osh.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
