namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public class PageInfoModel
    {
        public int FilteredItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}