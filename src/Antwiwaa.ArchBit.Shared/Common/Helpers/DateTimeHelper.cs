using System;

namespace Antwiwaa.ArchBit.Shared.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static string GetFormattedLongDateString(this DateTime date)
        {
            return date.ToString("D");
        }

        public static string GetFormattedShortDateString(this DateTime date)
        {
            return date.ToString("d");
        }

        public static int GetAge(this DateTime date)
        {
            var today = DateTime.Today;
            var age = today.Year - date.Year;

            if (date > today.AddYears(-age)) age--;

            return age;


            // try
            // {
            //     var yearDifference = DateTime.UtcNow.Year - date.Year;
            //     var currentBirthDay = new DateTime(DateTime.UtcNow.Year, date.Month, date.Day);
            //     return currentBirthDay > DateTime.UtcNow ? yearDifference - 1 : yearDifference;
            // }
            // catch (Exception)
            // {
            //     return 0;
            // }
        }

        public static string GetDate(this DateTime date)
        {
            return date.Date.ToString("d").Replace(" 00:00:00", "");
        }

        public static string GetDateWithoutTime(this DateTime date)
        {
            return date.Date.ToString("yy-MM-dd");
        }
    }
}