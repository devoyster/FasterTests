using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FasterTests.Helpers
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            return source.ToList().AsReadOnly();
        }
    }
}