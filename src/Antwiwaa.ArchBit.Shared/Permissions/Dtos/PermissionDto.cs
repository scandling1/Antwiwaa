using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Interfaces;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class PermissionDto : PayLoadObject, IHasRowNumber
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public string LocalizationKey { get; set; }
        public int? ParentPermissionId { get; set; }
        public string ParentPermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public string PermissionName { get; set; }
        public bool RequireAdminRole { get; set; }
        public int RowNumber { get; set; }
    }
}