using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Interfaces;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class RoleDto : PayLoadObject, IHasRowNumber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoleStatus RoleStatus { get; set; }
        public IList<RolePermissionDto> RolePermissions { get; set; }
        public int RowNumber { get; set; }
    }
}