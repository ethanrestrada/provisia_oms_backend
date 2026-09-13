using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("company");

            builder.HasKey(c => c.CompanyId);

            builder.Property(c => c.CompanyId)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.TaxId)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.LegalAddress)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.CreditLimit)
                .IsRequired()
                .HasPrecision(14, 2);

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            builder.HasIndex(c => c.TaxId)
                .IsUnique()
                .HasFilter("deleted_at IS NULL");
        }
    }
}
