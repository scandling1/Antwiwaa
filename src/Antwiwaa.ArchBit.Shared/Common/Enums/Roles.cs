using Antwiwaa.ArchBit.Shared.Common.Attributes;

namespace Antwiwaa.ArchBit.Shared.Common.Enums
{
    public enum Roles
    {
        None,
        [StringValue("Cube Admin")] SuperAdmin,
        [StringValue("Admin")] Admin,
        [StringValue("User")] User
    }
}