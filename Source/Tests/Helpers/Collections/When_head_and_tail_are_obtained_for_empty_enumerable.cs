using System.Collections.Generic;
using Machine.Specifications;
using FasterTests.Helpers.Collections;
using System.Linq;

namespace FasterTests.Tests.Helpers.Collections
{
    [Subject(typeof(EnumerableExtensions))]
    public class When_head_and_tail_are_obtained_for_empty_enumerable
    {
        Establish context = () =>
            enumerable = Enumerable.Empty<int>();

        Because of = () =>
            headAndTail = enumerable.ToHeadAndTail();

        It should_have_none_head = () => headAndTail.Head.IsSome.ShouldBeFalse();

        It should_use_original_enumerable_as_tail = () => headAndTail.Tail.ShouldEqual(enumerable);

        private static IEnumerable<int> InfiniteRange(int start)
        {
            while (true)
            {
                yield return start++;
            }
        }

        private static IEnumerable<int> enumerable;
        private static HeadAndTail<int> headAndTail;
    }
}