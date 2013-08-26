using System.Collections.Generic;
using System.Linq;
using FasterTests.Helpers.Collections;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers.Collections
{
    [Subject(typeof(EnumerableExtensions))]
    public class When_foreach_is_executed
    {
        Establish context = () =>
        {
            enumerable = Enumerable.Range(3, 2);
            list = new List<int>();
        };

        Because of = () =>
            enumerable.ForEach(list.Add);

        It should_enumerate_all_items = () => list.Count.ShouldEqual(2);

        It should_process_first_item_first = () => list[0].ShouldEqual(3);

        It should_process_second_item_next = () => list[1].ShouldEqual(4);

        private static IEnumerable<int> enumerable;
        private static List<int> list;
    }
}