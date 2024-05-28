using System;
using System.Collections.Generic;
using System.Linq;

namespace Janet.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Where(predicate);
        }

        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> selector)
        {
            return enumerable.Select(selector);
        }
    }
}