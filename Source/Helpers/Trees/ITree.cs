using System.Collections.Generic;

namespace FasterTests.Helpers.Trees
{
    public interface ITree<T>
    {
        T Value { get; }

        Tree<T> Parent { get; set; }

        IEnumerable<ITree<T>> Children { get; }

        ITree<T> Add(T value);

        void Add(ITree<T> node);
    }
}