using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("outbox_message");

            builder.HasKey(e => e.OutboxMessageId);

            builder.Property(e => e.EventType)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(e => e.Payload)
                  .IsRequired()
                  .HasMaxLength(20);
            
            builder.Property(e => e.ProcessedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Error)
                  .HasMaxLength(20);
        }
    }
}
