using FasterTests.Helpers.Trees;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Helpers.Trees
{
    [Subject(typeof(Tree<>))]
    public class When_tree_node_is_added
    {
        Establish context = () =>
        {
            root = Tree.Root(1);
            node = new Tree<int>(2);
        };

        Because of = () =>
            root.Add(node);

        It should_have_root_as_parent = () => node.Parent.ShouldBeTheSameAs(root);

        It should_be_added_as_root_child = () => root.Children.Single().ShouldBeTheSameAs(node);

        private static ITree<int> root;
        private static ITree<int> node;
    }
}