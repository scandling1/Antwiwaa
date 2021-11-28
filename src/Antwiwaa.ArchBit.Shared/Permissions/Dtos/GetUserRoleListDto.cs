using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Models;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class GetUserRoleListDto : PayLoadObject
    {
        public GetUserRoleListDto()
        {
            UserRoleDtos = new List<UserRoleDto>();
        }

        public IEnumerable<UserRoleDto> UserRoleDtos { get; set; }
    }
}