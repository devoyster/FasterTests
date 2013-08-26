using System.Collections.Generic;
using FasterTests.Helpers.Trees;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Helpers.Trees
{
    [Subject(typeof(TreeExtensions))]
    public class When_tree_branch_is_taken_by_always_false_predicate
    {
        Establish context = () =>
            tree = new Tree<int>(1)
                       {
                           10,
                           20,
                           new Tree<int>(2) { 3, 4, 5 }
                       };

        Because of = () =>
            branch = tree.ToBranchWhile(_ => false).ToList();

        It should_be_empty = () => branch.ShouldBeEmpty();

        private static Tree<int> tree;
        private static List<int> branch;
    }
}