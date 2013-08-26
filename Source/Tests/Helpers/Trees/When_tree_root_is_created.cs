using FasterTests.Helpers.Trees;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers.Trees
{
    [Subject(typeof(Tree<>))]
    public class When_tree_root_is_created
    {
        Because of = () =>
            root = Tree.Root(1);

        It should_have_no_parent = () => root.Parent.ShouldBeNull();

        It should_have_no_children = () => root.Children.ShouldBeEmpty();

        It should_have_supplied_value = () => root.Value.ShouldEqual(1);

        private static ITree<int> root;
    }
}