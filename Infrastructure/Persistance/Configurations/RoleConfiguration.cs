using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("role");

            builder.HasKey(r => r.RoleId);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.Property(r => r.Description)
                .HasMaxLength(255);

            builder.HasIndex(r => r.Name).IsUnique();
        }
    }
}
