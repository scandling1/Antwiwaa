using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Enums;

namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public class PermissionClaim
    {
        public Permission DependentPermission { get; set; }
        public IList<EntityTypes> EntityTypes { get; set; }
        public string PermissioName { get; set; }
        public bool RequireAdminRole { get; set; }
    }
}