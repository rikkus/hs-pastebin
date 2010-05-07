using System;
using System.Collections.Generic;

namespace HS.PasteBin.Web
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static void EachWithIndex<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            int i = 0;

            foreach (var item in collection)
            {
                action(item, i++);
            }
        }
    }
}
