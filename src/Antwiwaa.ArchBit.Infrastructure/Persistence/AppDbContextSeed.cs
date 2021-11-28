using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate;
using Antwiwaa.ArchBit.Shared.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Permission = Antwiwaa.ArchBit.Shared.Common.Enums.Permission;

namespace Antwiwaa.ArchBit.Infrastructure.Persistence
{
    public static class AppDbContextSeed
    {
        public static async Task SeedSampleDataAsync(AppDbContext context, IConfiguration configuration,
            CancellationToken cancellationToken)
        {
            #region Seed Permissions

            var permissions = Enum.GetValues<Permission>();

            if (permissions.Length != await context.Permissions.CountAsync(cancellationToken))
                foreach (var permission in permissions.Where(x => x != Permission.None))
                {
                    var permissionName = Enum.GetName(permission);

                    var attributeValue = permission.GetPermissionClaimValues();

                    var requiredAdmin = attributeValue.RequireAdminRole;

                    var existingPermission =
                        await context.Permissions.FirstOrDefaultAsync(x => x.PermissionName.Equals(permissionName),
                            cancellationToken);

                    if (existingPermission != null) continue;

                    var parentPermission = attributeValue.DependentPermission;

                    if (parentPermission == Permission.None)
                    {
                        await context.AddAsync(
                            (object)Domain.Entities.PermissionAggregate.Permission.New(permissionName,
                                permissionName.ToSpacedTitleCase(), $"{permissionName}Key", requiredAdmin),
                            cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);

                        continue;
                    }

                    var parentPermissionName = Enum.GetName(parentPermission);

                    var parentAttributeValue = parentPermission.GetPermissionClaimValues();

                    var parentRequiredAdmin = parentAttributeValue.RequireAdminRole;

                    var existingParent =
                        await context.Permissions.FirstOrDefaultAsync(
                            x => x.PermissionName.Equals(parentPermissionName), cancellationToken);

                    if (existingParent != null)
                    {
                        existingParent.AddChildPermission(Domain.Entities.PermissionAggregate.Permission.New(
                            permissionName, permissionName.ToSpacedTitleCase(), $"{permissionName}Key", requiredAdmin,
                            existingParent.Id));
                        await context.SaveChangesAsync(cancellationToken);

                        continue;
                    }

                    var newParent = Domain.Entities.PermissionAggregate.Permission.New(parentPermissionName,
                        parentPermissionName.ToSpacedTitleCase(), $"{parentPermissionName}Key", parentRequiredAdmin);
                    newParent.AddChildPermission(Domain.Entities.PermissionAggregate.Permission.New(permissionName,
                        permissionName.ToSpacedTitleCase(), $"{permissionName}Key", requiredAdmin));
                    await context.Permissions.AddAsync(newParent, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);
                }

            #endregion

            #region Setup Admin Account

            var userId = configuration["AdminConfig:AdminUser:UserId"];
            var userName = configuration["AdminConfig:AdminUser:UserName"];

            var userPermissionExist =
                await context.UserPermissions.AnyAsync(x => x.UserId == userId, cancellationToken);

            if (!userPermissionExist)
            {
                var adminPermissions = await context.Permissions.ToListAsync(cancellationToken);

                foreach (var adminPermission in adminPermissions)
                    adminPermission.AddUser(UserPermission.CreateUserPermission(userId, adminPermission.Id, userName));

                await context.SaveChangesAsync(cancellationToken);
            }

            #endregion
        }
    }
}