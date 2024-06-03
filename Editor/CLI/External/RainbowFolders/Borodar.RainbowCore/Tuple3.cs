/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace AIO.RainbowCore
{
    internal sealed class Tuple<T1, T2, T3>
    {
        private readonly T1 item1;

        private readonly T2 item2;

        private readonly T3 item3;

        public T1 Item1 => item1;

        public T2 Item2 => item2;

        public T3 Item3 => item3;

        public Tuple(T1 item1, T2 item2, T3 item3)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
        }

        public override int GetHashCode()
        {
            const int num0 = 17 * 23;
            var num1 = (num0 + (item1 != null ? item1.GetHashCode() : 0)) * 23;
            var num2 = (num1 + (item2 != null ? item2.GetHashCode() : 0)) * 23;
            var num3 = (num2 + (item3 != null ? item3.GetHashCode() : 0));
            return num3;
        }

        public override bool Equals(object o)
        {
            if (!(o is Tuple<T1, T2, T3>))
            {
                return false;
            }

            Tuple<T1, T2, T3> tuple = (Tuple<T1, T2, T3>)o;
            return this == tuple;
        }

        public static bool operator ==(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;

            if (a.item1 == null && b.item1 != null) return false;
            if (a.item1 != null && !a.item1.Equals(b.item1)) return false;

            if (a.item2 == null && b.item2 != null) return false;
            if (a.item2 != null && !a.item2.Equals(b.item2)) return false;

            if (a.item3 == null && b.item3 != null) return false;
            if (a.item3 != null && !a.item3.Equals(b.item3)) return false;

            return true;
        }

        public static bool operator !=(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
        {
            return !(a == b);
        }

        public void Unpack(Action<T1, T2, T3> unpackerDelegate)
        {
            unpackerDelegate(Item1, Item2, Item3);
        }
    }
}