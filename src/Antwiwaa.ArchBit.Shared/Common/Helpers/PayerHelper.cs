using System.Text.RegularExpressions;

namespace Antwiwaa.ArchBit.Shared.Common.Helpers
{
    public static class PayerHelper
    {
        public static int GetPayerSource(this string status)
        {
            return status == null ? 4 : 2;
        }

        public static bool IsAlpha(string strToCheck)
        {
            var rg = new Regex(@"^[a-zA-Z\s,]*$");
            return rg.IsMatch(strToCheck);
        }
    }
}