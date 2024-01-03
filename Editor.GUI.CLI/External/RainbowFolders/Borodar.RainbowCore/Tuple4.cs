/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-01-03
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;

namespace AIO.RainbowCore
{
    internal sealed class Tuple<T1, T2, T3, T4>
    {
        private readonly T1 item1;

        private readonly T2 item2;

        private readonly T3 item3;

        private readonly T4 item4;

        public T1 Item1 => item1;

        public T2 Item2 => item2;

        public T3 Item3 => item3;

        public T4 Item4 => item4;

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
        }

        public override int GetHashCode()
        {
            int num = 17 * 23;
            int num2;
            if (item1 != null)
            {
                T1 val = item1;
                num2 = val.GetHashCode();
            }
            else
            {
                num2 = 0;
            }

            int num3 = (num + num2) * 23;
            int num4;
            if (item2 != null)
            {
                T2 val2 = item2;
                num4 = val2.GetHashCode();
            }
            else
            {
                num4 = 0;
            }

            int num5 = (num3 + num4) * 23;
            int num6;
            if (item3 != null)
            {
                T3 val3 = item3;
                num6 = val3.GetHashCode();
            }
            else
            {
                num6 = 0;
            }

            int num7 = (num5 + num6) * 23;
            int num8;
            if (item4 != null)
            {
                T4 val4 = item4;
                num8 = val4.GetHashCode();
            }
            else
            {
                num8 = 0;
            }

            return num7 + num8;
        }

        public override bool Equals(object o)
        {
            if (o.GetType() != typeof(Tuple<T1, T2, T3, T4>))
            {
                return false;
            }

            Tuple<T1, T2, T3, T4> tuple = (Tuple<T1, T2, T3, T4>)o;
            return this == tuple;
        }

        public static bool operator ==(Tuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
        {
            if ((object)a == null)
            {
                return (object)b == null;
            }

            if (a.item1 == null && b.item1 != null)
            {
                return false;
            }

            if (a.item2 == null && b.item2 != null)
            {
                return false;
            }

            if (a.item3 == null && b.item3 != null)
            {
                return false;
            }

            if (a.item4 == null && b.item4 != null)
            {
                return false;
            }

            T1 val = a.item1;
            if (val.Equals(b.item1))
            {
                T2 val2 = a.item2;
                if (val2.Equals(b.item2))
                {
                    T3 val3 = a.item3;
                    if (val3.Equals(b.item3))
                    {
                        T4 val4 = a.item4;
                        return val4.Equals(b.item4);
                    }
                }
            }

            return false;
        }

        public static bool operator !=(Tuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
        {
            return !(a == b);
        }

        public void Unpack(Action<T1, T2, T3, T4> unpackerDelegate)
        {
            unpackerDelegate(Item1, Item2, Item3, Item4);
        }
    }
}