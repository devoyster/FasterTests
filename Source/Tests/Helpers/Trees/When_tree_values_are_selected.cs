using System.Collections.Generic;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Helpers.Trees
{
    [Subject(typeof(TreeExtensions))]
    public class When_tree_values_are_selected
    {
        Establish context = () =>
            root = new Tree<int>(1) { 2, 3 };

        Because of = () =>
            values = root.Children.Values().ToList();

        It should_return_two_values = () => values.Count.ShouldEqual(2);

        It should_return_first_value = () => values[0].ShouldEqual(2);

        It should_return_second_value = () => values[1].ShouldEqual(3);

        private static Tree<int> root;
        private static List<int> values;
    }
}