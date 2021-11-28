using System;
using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Enums;

namespace Antwiwaa.ArchBit.Shared.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class PermissionClaimsAttribute : Attribute
    {
        public PermissionClaimsAttribute(bool requireAdminRole) : this(requireAdminRole, null)
        {
        }


        public PermissionClaimsAttribute(bool requireAdminRole, IList<EntityTypes> entityTypes) : this(Permission.None,
            entityTypes, requireAdminRole)
        {
        }


        public PermissionClaimsAttribute(Permission dependentPermission) : this(dependentPermission, null)
        {
        }

        public PermissionClaimsAttribute(IList<EntityTypes> entityTypes) : this(Permission.None, entityTypes)
        {
        }


        public PermissionClaimsAttribute(Permission dependentPermission, IList<EntityTypes> entityTypes) : this(
            dependentPermission, entityTypes, false)
        {
        }

        public PermissionClaimsAttribute(Permission dependentPermission, bool requireAdminRole) : this(
            dependentPermission, null, requireAdminRole)
        {
        }

        public PermissionClaimsAttribute(Permission dependentPermission, IList<EntityTypes> entityTypes,
            bool requireAdminRole = false)
        {
            RequireAdminRole = requireAdminRole;
            DependentPermission = dependentPermission;
            EntityTypes = entityTypes;
        }

        public PermissionClaimsAttribute()
        {
        }

        public Permission DependentPermission { get; protected set; }

        public IList<EntityTypes> EntityTypes { get; protected set; }
        public bool RequireAdminRole { get; protected set; }
    }
}