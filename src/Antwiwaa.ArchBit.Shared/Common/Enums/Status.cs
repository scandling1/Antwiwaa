using Antwiwaa.ArchBit.Shared.Common.Attributes;

namespace Antwiwaa.ArchBit.Shared.Common.Enums
{
    public enum Status
    {
        None,
        [IntValue(1)] Active,
        [IntValue(2)] Inactive
    }
}