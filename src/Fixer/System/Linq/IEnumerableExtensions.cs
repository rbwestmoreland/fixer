using System.Collections.Generic;

namespace System.Linq
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }

            return items;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T, long> action)
        {
            for (int i = 0; i < items.LongCount(); i++)
            {
                var item = items.ElementAt(i);
                action(item, i);
            }

            return items;
        }
    }
}
