using System.Collections.Generic;
using FasterTests.Helpers.Collections;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers.Collections
{
    [Subject(typeof(EnumerableExtensions))]
    public class When_enumerable_is_created_from_enumerator : WithFakes
    {
        Establish context = () =>
            enumerator = An<IEnumerator<int>>();

        Because of = () =>
            enumerable = enumerator.ToEnumerable();

        It should_be_lazy = () => enumerator.WasNotToldTo(e => e.MoveNext());

        private static IEnumerator<int> enumerator;
        private static IEnumerable<int> enumerable;
    }
}