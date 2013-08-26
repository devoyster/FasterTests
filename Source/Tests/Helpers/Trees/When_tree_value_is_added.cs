using FasterTests.Helpers.Trees;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Helpers.Trees
{
    [Subject(typeof(Tree<>))]
    public class When_tree_value_is_added
    {
        Establish context = () =>
            root = Tree.Root(1);

        Because of = () =>
            node = root.Add(2);

        It should_have_root_as_parent = () => node.Parent.ShouldBeTheSameAs(root);

        It should_have_no_children = () => node.Children.ShouldBeEmpty();

        It should_have_supplied_value = () => node.Value.ShouldEqual(2);

        It should_be_added_as_root_child = () => root.Children.Single().ShouldBeTheSameAs(node);

        private static ITree<int> root;
        private static ITree<int> node;
    }
}