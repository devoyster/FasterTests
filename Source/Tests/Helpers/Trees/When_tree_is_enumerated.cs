using System;
using System.Collections;
using FasterTests.Helpers.Trees;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers.Trees
{
    [Subject(typeof(Tree<>))]
    public class When_tree_is_enumerated
    {
        Establish context = () =>
            tree = new Tree<int>(1);

        Because of = () =>
            exception = Catch.Exception(() => ((IEnumerable)tree).GetEnumerator());

        It should_fail = () => exception.ShouldBeOfExactType<InvalidOperationException>();

        private static Tree<int> tree;
        private static Exception exception;
    }
}