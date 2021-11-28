using System;
using System.Collections.Generic;
using System.Linq;
using Antwiwaa.ArchBit.Shared.Common.Enums;

namespace Antwiwaa.ArchBit.Shared.Common.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class PolicyClaimAttribute : Attribute
    {
        public PolicyClaimAttribute(Roles[] requiredRoles, Permission[] requiredPermissions)
        {
            RequiredPermissions = requiredPermissions.ToList();
            RequiredRoles = requiredRoles.ToList();
        }

        public IList<Permission> RequiredPermissions { get; set; }
        public IList<Roles> RequiredRoles { get; set; }
    }
}