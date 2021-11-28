using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Enums;

namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public class DataTableListRequestModel : PayLoadObject
    {
        public DataReadMode DataReadMode { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IList<Parameter> Parameters { get; set; }
    }

    public class Parameter
    {
        public string Field { get; set; }
        public string SearchValue { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}