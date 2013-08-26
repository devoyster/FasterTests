using System;

namespace FasterTests.Helpers
{
    public struct Option<T>
    {
        private readonly bool _isSome;
        private readonly T _value;

        public Option(bool isSome, T value = default(T))
        {
            _isSome = isSome;
            _value = value;
        }

        public bool IsSome
        {
            get { return _isSome; }
        }

        public T Value
        {
            get
            {
                if (!_isSome)
                {
                    throw new InvalidOperationException("Option value is undefined");
                }
                return _value;
            }
        }
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value)
        {
            return new Option<T>(true, value);
        }

        public static Option<T> None<T>()
        {
            return new Option<T>(false);
        }
    }
}