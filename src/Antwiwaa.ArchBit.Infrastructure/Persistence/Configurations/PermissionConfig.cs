using Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Antwiwaa.ArchBit.Infrastructure.Persistence.Configurations
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.PermissionName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PermissionDescription).IsRequired().HasMaxLength(120);
            builder.Property(x => x.LocalizationKey).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.RequireAdminRole).IsRequired();
        }
    }
}