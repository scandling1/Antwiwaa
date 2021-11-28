using Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Antwiwaa.ArchBit.Infrastructure.Persistence.Configurations
{
    public class UserPermissionConfig : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PermissionId).IsRequired();
        }
    }
}