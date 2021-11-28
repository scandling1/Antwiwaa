using Antwiwaa.ArchBit.Shared.Common.Attributes;

namespace Antwiwaa.ArchBit.Shared.Common.Enums
{
    public enum Policies
    {
        None,

        [PolicyClaim(new[] { Roles.SuperAdmin },
            new[] { Permission.ListPermission, Permission.UpdatePermission, Permission.DeactivatePermission })]
        CanListPermission,

        [PolicyClaim(new[] { Roles.SuperAdmin }, new[] { Permission.DeactivatePermission })]
        CanDeactivatePermission,

        [PolicyClaim(new[] { Roles.SuperAdmin }, new[] { Permission.AddPermission })]
        CanAddPermission,
        
    }
}