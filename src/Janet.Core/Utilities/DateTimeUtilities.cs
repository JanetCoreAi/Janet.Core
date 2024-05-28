using System;
using System.Globalization;

namespace Janet.Core.Utilities
{
    public static class DateTimeUtilities
    {
        public static int CalculateAge(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
                age--;

            return age;
        }

        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Monday)
        {
            int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        public static bool TryParseExact(string dateString, string format, out DateTime result)
        {
            return DateTime.TryParseExact(dateString, format, null, DateTimeStyles.None, out result);
        }
    }
}