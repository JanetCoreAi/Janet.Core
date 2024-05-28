using System;

namespace Janet.Core.Extensions
{
    public static class IntegerExtensions
    {
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