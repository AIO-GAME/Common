using System;
using System.Collections;

namespace AIO.RainbowCore
{
    internal static class Tuple
    {
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }

        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }

        public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

        public static void Unpack<T1, T2>(this Tuple<T1, T2> tuple, out T1 ref1, out T2 ref2)
        {
            ref1 = tuple.Item1;
            ref2 = tuple.Item2;
        }

        public static void Unpack<T1, T2, T3>(this Tuple<T1, T2, T3> tuple, out T1 ref1, out T2 ref2, T3 ref3)
        {
            ref1 = tuple.Item1;
            ref2 = tuple.Item2;
            ref3 = tuple.Item3;
        }

        public static void Unpack<T1, T2, T3, T4>(this Tuple<T1, T2, T3, T4> tuple, out T1 ref1, out T2 ref2, T3 ref3,
            T4 ref4)
        {
            ref1 = tuple.Item1;
            ref2 = tuple.Item2;
            ref3 = tuple.Item3;
            ref4 = tuple.Item4;
        }
    }

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

            if (object.Equals(a.Item1, b.Item1))
            {
                return object.Equals(a.Item2, b.Item2);
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