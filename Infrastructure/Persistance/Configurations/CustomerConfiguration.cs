using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer");

            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerId)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.CustomerType)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.CustomerTaxId)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            builder.HasIndex(c => c.CustomerTaxId)
                .IsUnique()
                .HasFilter("deleted_at IS NULL");

            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Company)
                .WithMany()
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
