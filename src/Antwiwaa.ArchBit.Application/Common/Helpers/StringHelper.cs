using System.Linq;
using System.Text.RegularExpressions;

namespace Antwiwaa.ArchBit.Application.Common.Helpers
{
    public static class StringHelper
    {
        public static bool NotContainsSpace(this string source)
        {
            if (string.IsNullOrEmpty(source)) return false;

            return !source.Any(char.IsWhiteSpace);
        }

        public static string RemoveNewLines(this string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            return Regex.Replace(source, @"\t|\n|\r", "");
        }


        public static string[] SplitToArray(this string source)
        {
            var result = string.IsNullOrEmpty(source) ? new[] { string.Empty } : source.Split(',').ToArray();

            return result;
        }
    }
}