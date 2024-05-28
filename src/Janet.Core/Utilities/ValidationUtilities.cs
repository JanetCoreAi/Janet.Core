using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Janet.Core.Utilities
{
    public static class ValidationUtilities
    {
        public static bool IsValidEmail(this string email)
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidUrl(this string url)
        {
            string pattern = @"^(https?://)?([\da-z\.-]+)\.([a-z\.]{2,6})([/\w\.-]*)*/?$";
            return Regex.IsMatch(url, pattern);
        }

        public static bool IsValidIpAddress(this string ipAddress)
        {
            IPAddress ip;
            return IPAddress.TryParse(ipAddress, out ip);
        }

        public static bool IsValidGuid(this string guidString)
        {
            Guid guid;
            return Guid.TryParse(guidString, out guid);
        }
    }
}