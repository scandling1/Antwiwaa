using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Antwiwaa.ArchBit.Shared.Common.Models
{
    public class ExceptionDetails
    {
        [JsonPropertyName("errors")] public Dictionary<string, string[]> Errors { get; set; }

        [JsonPropertyName("status")] public int Status { get; set; }

        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("detail")] public string Detail { get; set; }

        public string GetErrorsAsString()
        {
            var errorString = new StringBuilder();

            foreach (var (key, value) in Errors)
            {
                var errorSplit = key.Split(':');
                errorString.AppendLine($"{key} : {string.Join(',', value.Select(x => x).ToList())}");
            }

            return errorString.ToString();
        }
    }
}