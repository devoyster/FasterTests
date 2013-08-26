using System;
using System.Collections;
using System.Collections.Generic;

namespace FasterTests.Helpers.Trees
{
    public class Tree<T> : ITree<T>, IEnumerable
    {
        private readonly T _value;
        private readonly List<ITree<T>> _children;

        public Tree(T value)
        {
            _value = value;
            _children = new List<ITree<T>>();
        }

        public T Value
        {
            get { return _value; }
        }

        public Tree<T> Parent { get; set; }

        public IEnumerable<ITree<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public ITree<T> Add(T value)
        {
            var node = new Tree<T>(value);
            Add(node);
            return node;
        }

        public void Add(ITree<T> node)
        {
            node.Parent = this;
            _children.Add(node);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new InvalidOperationException("Do not use enumerator on tree");
        }
    }

    public static class Tree
    {
        public static ITree<T> Root<T>(T value)
        {
            return new Tree<T>(value);
        }
    }
}