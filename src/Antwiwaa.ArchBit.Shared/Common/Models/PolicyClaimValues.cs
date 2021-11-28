using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Enums;

namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public class PolicyClaimValues
    {
        public string PolicyName { get; set; }
        public IList<Permission> RequiredPermissions { get; set; }
        public IList<Roles> RequiredRoles { get; set; }
    }
}