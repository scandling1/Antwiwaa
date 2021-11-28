using Antwiwaa.ArchBit.Shared.Common.Attributes;

namespace Antwiwaa.ArchBit.Shared.Common.Enums
{
    public enum ClaimTypes
    {
        [StringValue("role")] Roles,

        [StringValue("https://localhost:5001/roles")]
        RoleFromNamespace,
        [StringValue("Permissions")] Permissions,
        [StringValue("Organisation_Id")] SubscriptionClaim,
        [StringValue("middle_name")] MiddleName,
        [StringValue("given_name")] FirstName,
        [StringValue("family_name")] LastName,
        [StringValue("phone_number")] PhoneNumber,
        [StringValue("EntityType")] EntityType,
        [StringValue("name")] UserName,
        [StringValue("sub")] Sub,
        [StringValue("nickname")] Nickname
    }
}