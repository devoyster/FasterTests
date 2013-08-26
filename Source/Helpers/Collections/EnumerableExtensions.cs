using System;
using System.Collections.Generic;

namespace FasterTests.Helpers.Collections
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

        public static HeadAndTail<T> ToHeadAndTail<T>(this IEnumerable<T> source)
        {
            var enumerator = source.GetEnumerator();

            var head = enumerator.MoveNext()
                        ? Option.Some(enumerator.Current)
                        : Option.None<T>();

            return HeadAndTail.Create(head, enumerator.ToEnumerable());
        }

        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator)
        {
            using (enumerator)
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }
    }
}