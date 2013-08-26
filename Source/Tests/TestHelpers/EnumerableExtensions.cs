using System.Collections.Generic;
using FasterTests.Helpers.Collections;

namespace FasterTests.Tests.TestHelpers
{
    public static class EnumerableExtensions
    {
        public static void ConsumeAll<T>(this IEnumerable<T> source)
        {
            source.ForEach(_ => {});
        }
    }
}