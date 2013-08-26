using System.Collections.Generic;
using FasterTests.Helpers.Collections;
using Machine.Fakes;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Helpers.Collections
{
    [Subject(typeof(EnumerableExtensions))]
    public class When_enumerable_created_from_enumerator_is_used : WithFakes
    {
        Establish context = () =>
        {
            enumerator = An<IEnumerator<int>>();
            enumerable = enumerator.ToEnumerable();
        };

        Because of = () =>
            enumerable.Any();

        It should_use_underlying_enumerator = () => enumerator.WasToldTo(e => e.MoveNext());

        private static IEnumerator<int> enumerator;
        private static IEnumerable<int> enumerable;
    }
}