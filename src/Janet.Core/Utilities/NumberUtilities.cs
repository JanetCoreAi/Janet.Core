using System;
using System.Globalization;

namespace Janet.Core.Utilities
{
    public static class NumberUtilities
    {
        public static string FormatCurrency(this decimal value, string culture = "en-US")
        {
            return value.ToString("C", new CultureInfo(culture));
        }

        public static string FormatNumber(this int value, string culture = "en-US")
        {
            return value.ToString("N", new CultureInfo(culture));
        }

        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(this int value)
        {
            return !value.IsEven();
        }

        public static string ToBinaryString(this int value)
        {
            return Convert.ToString(value, 2);
        }

        public static int FromBinaryString(this string binaryString)
        {
            return Convert.ToInt32(binaryString, 2);
        }
    }
}