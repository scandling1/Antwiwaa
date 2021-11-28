using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Models;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class GetRoleListDto : PayLoadObject
    {
        public GetRoleListDto()
        {
            RoleDtos = new List<RoleDto>();
        }

        public IEnumerable<RoleDto> RoleDtos { get; set; }
    }
}