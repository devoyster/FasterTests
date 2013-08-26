using System.Collections.Generic;

namespace FasterTests.Helpers.Collections
{
    public struct HeadAndTail<T>
    {
        private readonly Option<T> _head;
        private readonly IEnumerable<T> _tail;

        public HeadAndTail(Option<T> head, IEnumerable<T> tail)
        {
            _head = head;
            _tail = tail;
        }

        public Option<T> Head
        {
            get { return _head; }
        }

        public IEnumerable<T> Tail
        {
            get { return _tail; }
        }
    }

    public static class HeadAndTail
    {
        public static HeadAndTail<T> Create<T>(Option<T> head, IEnumerable<T> tail)
        {
           return new HeadAndTail<T>(head, tail);
        }
    }
}