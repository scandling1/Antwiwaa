using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Models;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class GetPermissionListDto : PayLoadObject
    {
        public GetPermissionListDto()
        {
            PermissionDtos = new List<PermissionDto>();
        }

        public IEnumerable<PermissionDto> PermissionDtos { get; set; }
    }
}