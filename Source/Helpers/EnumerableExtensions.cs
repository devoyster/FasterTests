using System;
using System.Collections.Generic;
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

        public static IEnumerable<IEnumerable<T>> SplitInEqualBatches<T>(this IEnumerable<T> source, int batchCount)
        {
            var sourceArray = source.ToArray();

            var batchSize = sourceArray.Length / batchCount + 1;
            var batches = Enumerable
                            .Repeat(1, batchCount)
                            .Select(_ => new List<T>(batchSize))
                            .ToArray();

            for (int i = 0; i < sourceArray.Length; i++)
            {
                batches[i / batchSize].Add(sourceArray[i]);
            }

            return batches;
        }
    }
}