using System;
using System.Collections.Generic;
using System.Linq;

namespace FasterTests.Helpers.Trees
{
    public static class TreeExtensions
    {
        public static IEnumerable<T> ToBranchWhile<T>(this ITree<T> tree, Func<T, bool> predicate)
        {
            if (!predicate(tree.Value))
            {
                tree = null;
            }

            while (tree != null)
            {
                yield return tree.Value;
                tree = tree.Children.FirstOrDefault(t => predicate(t.Value));
            }
        }

        public static IEnumerable<T> Values<T>(this IEnumerable<ITree<T>> source)
        {
            return source.Select(t => t.Value);
        }
    }
}