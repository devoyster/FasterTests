using System.Collections.Generic;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Helpers.Trees
{
    [Subject(typeof(TreeExtensions))]
    public class When_tree_branch_is_taken_by_predicate
    {
        Establish context = () =>
            tree = new Tree<int>(1)
                       {
                           10,
                           20,
                           new Tree<int>(2) { 3, 4, 5 }
                       };

        Because of = () =>
            branch = tree.ToBranchWhile(x => x < 10).ToList();

        It should_have_all_branch_nodes = () => branch.Count.ShouldEqual(3);

        It should_only_contain_nodes_matching_predicate = () => branch[1].ShouldEqual(2);

        It should_use_first_of_child_nodes_if_two_or_more_are_matching_predicate = () => branch[2].ShouldEqual(3);

        private static Tree<int> tree;
        private static List<int> branch;
    }
}