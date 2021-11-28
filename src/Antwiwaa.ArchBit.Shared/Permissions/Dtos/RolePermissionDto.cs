using Antwiwaa.ArchBit.Shared.Common.Models;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class RolePermissionDto : PayLoadObject
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        /*public RoleDto Role { get; set; }*/
        public int PermissionId { get; set; }
        /*public PermissionDto Permission { get; set; }*/
    }
}