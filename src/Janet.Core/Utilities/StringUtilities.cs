using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Janet.Core.Utilities
{
    public static class StringUtilities
    {
        public static string TrimStart(this string str, params char[] trimChars)
        {
            return str.TrimStart(trimChars);
        }

        public static string TrimEnd(this string str, params char[] trimChars)
        {
            return str.TrimEnd(trimChars);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string ToSnakeCase(this string str)
        {
            return Regex.Replace(str, @"(\p{Ll})(\p{Lu})", "$1_$2").ToLower();
        }

        public static string ToCamelCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (str.Length == 1)
                return str.ToLower();

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        public static string EncodeBase64(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string DecodeBase64(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}