using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("audit_log");

            builder.HasKey(al => al.AuditLogId);

            builder.Property(al => al.EntityName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(al => al.EntityId)
                .IsRequired();

            builder.Property(al => al.Action)
                .IsRequired()
                .HasMaxLength(510);

            builder.Property(al => al.Timestamp)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.HasIndex(al => al.EntityName);
            builder.HasIndex(al => al.Timestamp);

            builder.HasOne(al => al.User)
                .WithMany()
                .HasForeignKey(al => al.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
