using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Models;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class GetRolePermissionListDto : PayLoadObject
    {
        public GetRolePermissionListDto()
        {
            RolePermissionDtos = new List<RolePermissionDto>();
        }

        public IEnumerable<RolePermissionDto> RolePermissionDtos { get; set; }
    }
}