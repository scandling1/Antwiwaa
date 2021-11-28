using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Antwiwaa.ArchBit.Shared.Interfaces;
using Newtonsoft.Json;

namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public class DataTableVm<T> : PageInfoModel, IPayLoadObject where T : class, IHasRowNumber
    {
        public IEnumerable<T> Data { get; set; }

        public HttpContent GetHttpContent()
        {
            var inputJson = JsonConvert.SerializeObject(this);

            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");

            return inputContent;
        }


        private DataTableVm<T> GenerateRowNumbers()
        {
            var index = 0;

            foreach (var data in Data) data.RowNumber = ++index + (Page * PageSize - PageSize);

            return this;
        }

        public static DataTableVm<T> New(int totalItems, int page, int pageSize, IList<T> data)
        {
            var obj = new DataTableVm<T>
            {
                FilteredItems = data.Count,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                Data = data
            }.GenerateRowNumbers();

            return obj;
        }
    }
}