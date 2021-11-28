using Antwiwaa.ArchBit.Shared.Common.Models;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class UserRoleDto : PayLoadObject
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}