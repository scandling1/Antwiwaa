using Antwiwaa.ArchBit.Shared.Common.Attributes;

namespace Antwiwaa.ArchBit.Shared.Common.Enums
{
    public enum SortDirection
    {
        None,
        [StringValue("ASC")] Ascending,
        [StringValue("DESC")] Descending
    }
}