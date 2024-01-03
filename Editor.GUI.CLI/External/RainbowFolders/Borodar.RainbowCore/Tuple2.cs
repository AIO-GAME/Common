/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace AIO.RainbowCore
{
    internal sealed class Tuple<T1, T2> : IComparable
    {
        public T1 Item1 { get; }

        public T2 Item2 { get; }

        public Tuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public object this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Item1;
                    case 1: return Item2;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public override string ToString()
        {
            return $"Tuple({Item1}, {Item2})";
        }

        public int CompareTo(object obj)
        {
            if (obj is Tuple<T1, T2> tuple && tuple == this) return 0;
            return 1;
        }

        public int Length => 1;

        public override int GetHashCode()
        {
            var num0 = 17 * 23;
            var num1 = (num0 + (Item1 != null ? Item1.GetHashCode() : 0)) * 23;
            var num2 = (num1 + (Item2 != null ? Item2.GetHashCode() : 0));
            return num2;
        }

        public override bool Equals(object o)
        {
            if (!(o is Tuple<T1, T2>))
            {
                return false;
            }

            var tuple = (Tuple<T1, T2>)o;
            return this == tuple;
        }

        public bool Equals(Tuple<T1, T2> other)
        {
            return this == other;
        }

        public static bool operator ==(Tuple<T1, T2> a, Tuple<T1, T2> b)
        {
            if ((object)a == null)
            {
                return (object)b == null;
            }

            if ((object)b == null)
            {
                return false;
            }

            if (Equals(a.Item1, b.Item1))
            {
                return Equals(a.Item2, b.Item2);
            }

            return false;
        }

        public static bool operator !=(Tuple<T1, T2> a, Tuple<T1, T2> b)
        {
            return !(a == b);
        }

        public void Unpack(Action<T1, T2> unpackerDelegate)
        {
            unpackerDelegate(Item1, Item2);
        }
    }
}