using Antwiwaa.ArchBit.Shared.Common.Attributes;

namespace Antwiwaa.ArchBit.Shared.Common.Enums
{
    public enum EntityTypes
    {
        [StringValue("CCR")] [StringDescription("/Subscription/RegistrationApprovalDetails/")]
        Chamber,

        [StringValue("CTR")] [StringDescription("/Subscription/RegistrationApprovalDetails/")]
        Trader,

        [StringValue("CAR")] [StringDescription("/Subscription/RegistrationApprovalDetails/")]
        Agent,
        [StringValue("Vendor")] Vendor
    }
}