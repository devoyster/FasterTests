using System.Collections.Generic;
using Machine.Specifications;
using FasterTests.Helpers.Collections;
using System.Linq;

namespace FasterTests.Tests.Helpers.Collections
{
    [Subject(typeof(EnumerableExtensions))]
    public class When_head_and_tail_are_obtained
    {
        Establish context = () =>
            enumerable = InfiniteRange(1);

        Because of = () =>
            headAndTail = enumerable.ToHeadAndTail();

        It should_have_some_head = () => headAndTail.Head.IsSome.ShouldBeTrue();

        It should_have_first_item_as_head = () => headAndTail.Head.Value.ShouldEqual(1);

        It should_have_lazy_tail = () => {};

        It should_have_remaining_items_as_tail = () => headAndTail.Tail.First().ShouldEqual(2);

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