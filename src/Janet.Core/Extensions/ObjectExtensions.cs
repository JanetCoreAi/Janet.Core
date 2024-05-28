using System;

namespace Janet.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsNotNull(this object obj)
        {
            return !obj.IsNull();
        }

        public static bool IsOfType<T>(this object obj)
        {
            return obj is T;
        }
    }
}