using Antwiwaa.ArchBit.Shared.Common.Attributes;

namespace Antwiwaa.ArchBit.Shared.Common.Enums
{
    public enum Permission
    {
        None,
        [PermissionClaims(true)] ListPermission,

        [PermissionClaims(ListPermission, true)]
        UpdatePermission,

        [PermissionClaims(ListPermission, true)]
        AddPermission,

        [PermissionClaims(ListPermission, true)]
        DeactivatePermission,
        
    }
}