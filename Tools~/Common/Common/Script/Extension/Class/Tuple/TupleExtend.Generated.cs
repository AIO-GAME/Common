using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    partial class TupleExtend
    {
	    #region ToArray

        public static T[] ToArray<T>(this Tuple<T> item)
           => new T[] { item.Item1 };

        public static T[] ToArray<T>(this Tuple<T, T> item)
           => new T[] { item.Item1, item.Item2 };

        public static T[] ToArray<T>(this Tuple<T, T, T> item)
           => new T[] { item.Item1, item.Item2, item.Item3 };

        public static T[] ToArray<T>(this Tuple<T, T, T, T> item)
           => new T[] { item.Item1, item.Item2, item.Item3, item.Item4 };

        public static T[] ToArray<T>(this Tuple<T, T, T, T, T> item)
           => new T[] { item.Item1, item.Item2, item.Item3, item.Item4, item.Item5 };

        public static T[] ToArray<T>(this Tuple<T, T, T, T, T, T> item)
           => new T[] { item.Item1, item.Item2, item.Item3, item.Item4, item.Item5, item.Item6 };

        public static T[] ToArray<T>(this Tuple<T, T, T, T, T, T, T> item)
           => new T[] { item.Item1, item.Item2, item.Item3, item.Item4, item.Item5, item.Item6, item.Item7 };

	    #endregion

	    #region ToList

        public static List<T> ToList<T>(this Tuple<T> item)
        {
           var list = new List<T>();
           list.Add(item.Item1);
           return list;
        }

        public static List<T> ToList<T>(this Tuple<T, T> item)
        {
           var list = new List<T>();
           list.Add(item.Item1);
           list.Add(item.Item2);
           return list;
        }

        public static List<T> ToList<T>(this Tuple<T, T, T> item)
        {
           var list = new List<T>();
           list.Add(item.Item1);
           list.Add(item.Item2);
           list.Add(item.Item3);
           return list;
        }

        public static List<T> ToList<T>(this Tuple<T, T, T, T> item)
        {
           var list = new List<T>();
           list.Add(item.Item1);
           list.Add(item.Item2);
           list.Add(item.Item3);
           list.Add(item.Item4);
           return list;
        }

        public static List<T> ToList<T>(this Tuple<T, T, T, T, T> item)
        {
           var list = new List<T>();
           list.Add(item.Item1);
           list.Add(item.Item2);
           list.Add(item.Item3);
           list.Add(item.Item4);
           list.Add(item.Item5);
           return list;
        }

        public static List<T> ToList<T>(this Tuple<T, T, T, T, T, T> item)
        {
           var list = new List<T>();
           list.Add(item.Item1);
           list.Add(item.Item2);
           list.Add(item.Item3);
           list.Add(item.Item4);
           list.Add(item.Item5);
           list.Add(item.Item6);
           return list;
        }

        public static List<T> ToList<T>(this Tuple<T, T, T, T, T, T, T> item)
        {
           var list = new List<T>();
           list.Add(item.Item1);
           list.Add(item.Item2);
           list.Add(item.Item3);
           list.Add(item.Item4);
           list.Add(item.Item5);
           list.Add(item.Item6);
           list.Add(item.Item7);
           return list;
        }

	    #endregion

	    #region Tuple<T1, T2>

        public static ATuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new ATuple<T1, T2>(item1, item2);
        }

        public static void Unpack<T1, T2>(this ATuple<T1, T2> tuple, out T1 ref1, out T2 ref2)
        {
          ref1 = tuple.Item1;
          ref2 = tuple.Item2;
        }

	    #endregion

	    #region Tuple<T1, T2, T3>

        public static ATuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new ATuple<T1, T2, T3>(item1, item2, item3);
        }

        public static void Unpack<T1, T2, T3>(this ATuple<T1, T2, T3> tuple, out T1 ref1, out T2 ref2, out T3 ref3)
        {
          ref1 = tuple.Item1;
          ref2 = tuple.Item2;
          ref3 = tuple.Item3;
        }

	    #endregion

	    #region Tuple<T1, T2, T3, T4>

        public static ATuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new ATuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

        public static void Unpack<T1, T2, T3, T4>(this ATuple<T1, T2, T3, T4> tuple, out T1 ref1, out T2 ref2, out T3 ref3, out T4 ref4)
        {
          ref1 = tuple.Item1;
          ref2 = tuple.Item2;
          ref3 = tuple.Item3;
          ref4 = tuple.Item4;
        }

	    #endregion

	    #region Tuple<T1, T2, T3, T4, T5>

        public static ATuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            return new ATuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
        }

        public static void Unpack<T1, T2, T3, T4, T5>(this ATuple<T1, T2, T3, T4, T5> tuple, out T1 ref1, out T2 ref2, out T3 ref3, out T4 ref4, out T5 ref5)
        {
          ref1 = tuple.Item1;
          ref2 = tuple.Item2;
          ref3 = tuple.Item3;
          ref4 = tuple.Item4;
          ref5 = tuple.Item5;
        }

	    #endregion

	    #region Tuple<T1, T2, T3, T4, T5, T6>

        public static ATuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            return new ATuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
        }

        public static void Unpack<T1, T2, T3, T4, T5, T6>(this ATuple<T1, T2, T3, T4, T5, T6> tuple, out T1 ref1, out T2 ref2, out T3 ref3, out T4 ref4, out T5 ref5, out T6 ref6)
        {
          ref1 = tuple.Item1;
          ref2 = tuple.Item2;
          ref3 = tuple.Item3;
          ref4 = tuple.Item4;
          ref5 = tuple.Item5;
          ref6 = tuple.Item6;
        }

	    #endregion

	    #region Tuple<T1, T2, T3, T4, T5, T6, T7>

        public static ATuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            return new ATuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
        }

        public static void Unpack<T1, T2, T3, T4, T5, T6, T7>(this ATuple<T1, T2, T3, T4, T5, T6, T7> tuple, out T1 ref1, out T2 ref2, out T3 ref3, out T4 ref4, out T5 ref5, out T6 ref6, out T7 ref7)
        {
          ref1 = tuple.Item1;
          ref2 = tuple.Item2;
          ref3 = tuple.Item3;
          ref4 = tuple.Item4;
          ref5 = tuple.Item5;
          ref6 = tuple.Item6;
          ref7 = tuple.Item7;
        }

	    #endregion

}

    public sealed class ATuple<T1, T2> : IComparable
    {

	    /// <summary>
	    /// The number of items in the tuple.
	    /// </summary>
	    public int Length => 2;

        /// <summary> 参数1 </summary>
        public T1 Item1 { get; }

        /// <summary> 参数2 </summary>
        public T2 Item2 { get; }

	    /// <summary>
	    /// Initializes a new instance of the <see cref="Tuple&lt;T1, T2>"/> class.
	    /// </summary>
	    public ATuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
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

        /// <summary>
        ///
        /// </summary>
	    public override string ToString()
	    {
		    return $"Tuple({Item1}, {Item2})";
	    }

        /// <summary>
        ///
        /// </summary>
	    public override bool Equals(object o)
	    {
		    if (!(o is Tuple<T1, T2>))
		    {
			    return false;
		    }
		    var tuple = (Tuple<T1, T2>)o;
		    return this == tuple;
	    }

        /// <summary>
        ///
        /// </summary>
	    public bool Equals(Tuple<T1, T2> other)
	    {
		    return this == other;
	    }

        /// <summary>
        ///
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj is ATuple<T1, T2> tuple && tuple == this) return 0;
            return 1;
        }

        /// <summary>
        /// 作为默认哈希函数。
        /// </summary>
        /// <returns>当前的哈希代码</returns>
	    public override int GetHashCode()
	    {
		    const int num0 = 17 * 23;
		    var num1 = (num0 + (Item1 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num2 = (num1 + (Item2 != null ? Item1.GetHashCode() : 0));
		    return num2;
	    }

	    public static bool operator ==(ATuple<T1, T2> a, Tuple<T1, T2> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2> a, Tuple<T1, T2> b)
        {
            return !(a == b);
        }

	    public static bool operator ==(ATuple<T1, T2> a, ATuple<T1, T2> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2> a, ATuple<T1, T2> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 解包元组。
        /// </summary>
	    public void Unpack(Action<T1, T2> unpackerDelegate)
	    {
		    unpackerDelegate(Item1, Item2);
	    }
    }

    public sealed class ATuple<T1, T2, T3> : IComparable
    {

	    /// <summary>
	    /// The number of items in the tuple.
	    /// </summary>
	    public int Length => 3;

        /// <summary> 参数1 </summary>
        public T1 Item1 { get; }

        /// <summary> 参数2 </summary>
        public T2 Item2 { get; }

        /// <summary> 参数3 </summary>
        public T3 Item3 { get; }

	    /// <summary>
	    /// Initializes a new instance of the <see cref="Tuple&lt;T1, T2, T3>"/> class.
	    /// </summary>
	    public ATuple(T1 item1, T2 item2, T3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public object this[int index]
        {
            get
            {
                switch (index)
                {
				    case 0: return Item1;
				    case 1: return Item2;
				    case 2: return Item3;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
	    public override string ToString()
	    {
		    return $"Tuple({Item1}, {Item2}, {Item3})";
	    }

        /// <summary>
        ///
        /// </summary>
	    public override bool Equals(object o)
	    {
		    if (!(o is Tuple<T1, T2, T3>))
		    {
			    return false;
		    }
		    var tuple = (Tuple<T1, T2, T3>)o;
		    return this == tuple;
	    }

        /// <summary>
        ///
        /// </summary>
	    public bool Equals(Tuple<T1, T2, T3> other)
	    {
		    return this == other;
	    }

        /// <summary>
        ///
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj is ATuple<T1, T2, T3> tuple && tuple == this) return 0;
            return 1;
        }

        /// <summary>
        /// 作为默认哈希函数。
        /// </summary>
        /// <returns>当前的哈希代码</returns>
	    public override int GetHashCode()
	    {
		    const int num0 = 17 * 23;
		    var num1 = (num0 + (Item1 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num2 = (num1 + (Item2 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num3 = (num2 + (Item3 != null ? Item1.GetHashCode() : 0));
		    return num3;
	    }

	    public static bool operator ==(ATuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
        {
            return !(a == b);
        }

	    public static bool operator ==(ATuple<T1, T2, T3> a, ATuple<T1, T2, T3> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3> a, ATuple<T1, T2, T3> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 解包元组。
        /// </summary>
	    public void Unpack(Action<T1, T2, T3> unpackerDelegate)
	    {
		    unpackerDelegate(Item1, Item2, Item3);
	    }
    }

    public sealed class ATuple<T1, T2, T3, T4> : IComparable
    {

	    /// <summary>
	    /// The number of items in the tuple.
	    /// </summary>
	    public int Length => 4;

        /// <summary> 参数1 </summary>
        public T1 Item1 { get; }

        /// <summary> 参数2 </summary>
        public T2 Item2 { get; }

        /// <summary> 参数3 </summary>
        public T3 Item3 { get; }

        /// <summary> 参数4 </summary>
        public T4 Item4 { get; }

	    /// <summary>
	    /// Initializes a new instance of the <see cref="Tuple&lt;T1, T2, T3, T4>"/> class.
	    /// </summary>
	    public ATuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public object this[int index]
        {
            get
            {
                switch (index)
                {
				    case 0: return Item1;
				    case 1: return Item2;
				    case 2: return Item3;
				    case 3: return Item4;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
	    public override string ToString()
	    {
		    return $"Tuple({Item1}, {Item2}, {Item3}, {Item4})";
	    }

        /// <summary>
        ///
        /// </summary>
	    public override bool Equals(object o)
	    {
		    if (!(o is Tuple<T1, T2, T3, T4>))
		    {
			    return false;
		    }
		    var tuple = (Tuple<T1, T2, T3, T4>)o;
		    return this == tuple;
	    }

        /// <summary>
        ///
        /// </summary>
	    public bool Equals(Tuple<T1, T2, T3, T4> other)
	    {
		    return this == other;
	    }

        /// <summary>
        ///
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj is ATuple<T1, T2, T3, T4> tuple && tuple == this) return 0;
            return 1;
        }

        /// <summary>
        /// 作为默认哈希函数。
        /// </summary>
        /// <returns>当前的哈希代码</returns>
	    public override int GetHashCode()
	    {
		    const int num0 = 17 * 23;
		    var num1 = (num0 + (Item1 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num2 = (num1 + (Item2 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num3 = (num2 + (Item3 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num4 = (num3 + (Item4 != null ? Item1.GetHashCode() : 0));
		    return num4;
	    }

	    public static bool operator ==(ATuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
   		    if (a.Item4 == null && b.Item4 != null) return false;
            if (a.Item4 != null && !a.Item4.Equals(b.Item4)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
        {
            return !(a == b);
        }

	    public static bool operator ==(ATuple<T1, T2, T3, T4> a, ATuple<T1, T2, T3, T4> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
   		    if (a.Item4 == null && b.Item4 != null) return false;
            if (a.Item4 != null && !a.Item4.Equals(b.Item4)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3, T4> a, ATuple<T1, T2, T3, T4> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 解包元组。
        /// </summary>
	    public void Unpack(Action<T1, T2, T3, T4> unpackerDelegate)
	    {
		    unpackerDelegate(Item1, Item2, Item3, Item4);
	    }
    }

    public sealed class ATuple<T1, T2, T3, T4, T5> : IComparable
    {

	    /// <summary>
	    /// The number of items in the tuple.
	    /// </summary>
	    public int Length => 5;

        /// <summary> 参数1 </summary>
        public T1 Item1 { get; }

        /// <summary> 参数2 </summary>
        public T2 Item2 { get; }

        /// <summary> 参数3 </summary>
        public T3 Item3 { get; }

        /// <summary> 参数4 </summary>
        public T4 Item4 { get; }

        /// <summary> 参数5 </summary>
        public T5 Item5 { get; }

	    /// <summary>
	    /// Initializes a new instance of the <see cref="Tuple&lt;T1, T2, T3, T4, T5>"/> class.
	    /// </summary>
	    public ATuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
        }

        public object this[int index]
        {
            get
            {
                switch (index)
                {
				    case 0: return Item1;
				    case 1: return Item2;
				    case 2: return Item3;
				    case 3: return Item4;
				    case 4: return Item5;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
	    public override string ToString()
	    {
		    return $"Tuple({Item1}, {Item2}, {Item3}, {Item4}, {Item5})";
	    }

        /// <summary>
        ///
        /// </summary>
	    public override bool Equals(object o)
	    {
		    if (!(o is Tuple<T1, T2, T3, T4, T5>))
		    {
			    return false;
		    }
		    var tuple = (Tuple<T1, T2, T3, T4, T5>)o;
		    return this == tuple;
	    }

        /// <summary>
        ///
        /// </summary>
	    public bool Equals(Tuple<T1, T2, T3, T4, T5> other)
	    {
		    return this == other;
	    }

        /// <summary>
        ///
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj is ATuple<T1, T2, T3, T4, T5> tuple && tuple == this) return 0;
            return 1;
        }

        /// <summary>
        /// 作为默认哈希函数。
        /// </summary>
        /// <returns>当前的哈希代码</returns>
	    public override int GetHashCode()
	    {
		    const int num0 = 17 * 23;
		    var num1 = (num0 + (Item1 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num2 = (num1 + (Item2 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num3 = (num2 + (Item3 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num4 = (num3 + (Item4 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num5 = (num4 + (Item5 != null ? Item1.GetHashCode() : 0));
		    return num5;
	    }

	    public static bool operator ==(ATuple<T1, T2, T3, T4, T5> a, Tuple<T1, T2, T3, T4, T5> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
   		    if (a.Item4 == null && b.Item4 != null) return false;
            if (a.Item4 != null && !a.Item4.Equals(b.Item4)) return false;
   		    if (a.Item5 == null && b.Item5 != null) return false;
            if (a.Item5 != null && !a.Item5.Equals(b.Item5)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3, T4, T5> a, Tuple<T1, T2, T3, T4, T5> b)
        {
            return !(a == b);
        }

	    public static bool operator ==(ATuple<T1, T2, T3, T4, T5> a, ATuple<T1, T2, T3, T4, T5> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
   		    if (a.Item4 == null && b.Item4 != null) return false;
            if (a.Item4 != null && !a.Item4.Equals(b.Item4)) return false;
   		    if (a.Item5 == null && b.Item5 != null) return false;
            if (a.Item5 != null && !a.Item5.Equals(b.Item5)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3, T4, T5> a, ATuple<T1, T2, T3, T4, T5> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 解包元组。
        /// </summary>
	    public void Unpack(Action<T1, T2, T3, T4, T5> unpackerDelegate)
	    {
		    unpackerDelegate(Item1, Item2, Item3, Item4, Item5);
	    }
    }

    public sealed class ATuple<T1, T2, T3, T4, T5, T6> : IComparable
    {

	    /// <summary>
	    /// The number of items in the tuple.
	    /// </summary>
	    public int Length => 6;

        /// <summary> 参数1 </summary>
        public T1 Item1 { get; }

        /// <summary> 参数2 </summary>
        public T2 Item2 { get; }

        /// <summary> 参数3 </summary>
        public T3 Item3 { get; }

        /// <summary> 参数4 </summary>
        public T4 Item4 { get; }

        /// <summary> 参数5 </summary>
        public T5 Item5 { get; }

        /// <summary> 参数6 </summary>
        public T6 Item6 { get; }

	    /// <summary>
	    /// Initializes a new instance of the <see cref="Tuple&lt;T1, T2, T3, T4, T5, T6>"/> class.
	    /// </summary>
	    public ATuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
        }

        public object this[int index]
        {
            get
            {
                switch (index)
                {
				    case 0: return Item1;
				    case 1: return Item2;
				    case 2: return Item3;
				    case 3: return Item4;
				    case 4: return Item5;
				    case 5: return Item6;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
	    public override string ToString()
	    {
		    return $"Tuple({Item1}, {Item2}, {Item3}, {Item4}, {Item5}, {Item6})";
	    }

        /// <summary>
        ///
        /// </summary>
	    public override bool Equals(object o)
	    {
		    if (!(o is Tuple<T1, T2, T3, T4, T5, T6>))
		    {
			    return false;
		    }
		    var tuple = (Tuple<T1, T2, T3, T4, T5, T6>)o;
		    return this == tuple;
	    }

        /// <summary>
        ///
        /// </summary>
	    public bool Equals(Tuple<T1, T2, T3, T4, T5, T6> other)
	    {
		    return this == other;
	    }

        /// <summary>
        ///
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj is ATuple<T1, T2, T3, T4, T5, T6> tuple && tuple == this) return 0;
            return 1;
        }

        /// <summary>
        /// 作为默认哈希函数。
        /// </summary>
        /// <returns>当前的哈希代码</returns>
	    public override int GetHashCode()
	    {
		    const int num0 = 17 * 23;
		    var num1 = (num0 + (Item1 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num2 = (num1 + (Item2 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num3 = (num2 + (Item3 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num4 = (num3 + (Item4 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num5 = (num4 + (Item5 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num6 = (num5 + (Item6 != null ? Item1.GetHashCode() : 0));
		    return num6;
	    }

	    public static bool operator ==(ATuple<T1, T2, T3, T4, T5, T6> a, Tuple<T1, T2, T3, T4, T5, T6> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
   		    if (a.Item4 == null && b.Item4 != null) return false;
            if (a.Item4 != null && !a.Item4.Equals(b.Item4)) return false;
   		    if (a.Item5 == null && b.Item5 != null) return false;
            if (a.Item5 != null && !a.Item5.Equals(b.Item5)) return false;
   		    if (a.Item6 == null && b.Item6 != null) return false;
            if (a.Item6 != null && !a.Item6.Equals(b.Item6)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3, T4, T5, T6> a, Tuple<T1, T2, T3, T4, T5, T6> b)
        {
            return !(a == b);
        }

	    public static bool operator ==(ATuple<T1, T2, T3, T4, T5, T6> a, ATuple<T1, T2, T3, T4, T5, T6> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
   		    if (a.Item4 == null && b.Item4 != null) return false;
            if (a.Item4 != null && !a.Item4.Equals(b.Item4)) return false;
   		    if (a.Item5 == null && b.Item5 != null) return false;
            if (a.Item5 != null && !a.Item5.Equals(b.Item5)) return false;
   		    if (a.Item6 == null && b.Item6 != null) return false;
            if (a.Item6 != null && !a.Item6.Equals(b.Item6)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3, T4, T5, T6> a, ATuple<T1, T2, T3, T4, T5, T6> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 解包元组。
        /// </summary>
	    public void Unpack(Action<T1, T2, T3, T4, T5, T6> unpackerDelegate)
	    {
		    unpackerDelegate(Item1, Item2, Item3, Item4, Item5, Item6);
	    }
    }

    public sealed class ATuple<T1, T2, T3, T4, T5, T6, T7> : IComparable
    {

	    /// <summary>
	    /// The number of items in the tuple.
	    /// </summary>
	    public int Length => 7;

        /// <summary> 参数1 </summary>
        public T1 Item1 { get; }

        /// <summary> 参数2 </summary>
        public T2 Item2 { get; }

        /// <summary> 参数3 </summary>
        public T3 Item3 { get; }

        /// <summary> 参数4 </summary>
        public T4 Item4 { get; }

        /// <summary> 参数5 </summary>
        public T5 Item5 { get; }

        /// <summary> 参数6 </summary>
        public T6 Item6 { get; }

        /// <summary> 参数7 </summary>
        public T7 Item7 { get; }

	    /// <summary>
	    /// Initializes a new instance of the <see cref="Tuple&lt;T1, T2, T3, T4, T5, T6, T7>"/> class.
	    /// </summary>
	    public ATuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
            Item5 = item5;
            Item6 = item6;
            Item7 = item7;
        }

        public object this[int index]
        {
            get
            {
                switch (index)
                {
				    case 0: return Item1;
				    case 1: return Item2;
				    case 2: return Item3;
				    case 3: return Item4;
				    case 4: return Item5;
				    case 5: return Item6;
				    case 6: return Item7;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
	    public override string ToString()
	    {
		    return $"Tuple({Item1}, {Item2}, {Item3}, {Item4}, {Item5}, {Item6}, {Item7})";
	    }

        /// <summary>
        ///
        /// </summary>
	    public override bool Equals(object o)
	    {
		    if (!(o is Tuple<T1, T2, T3, T4, T5, T6, T7>))
		    {
			    return false;
		    }
		    var tuple = (Tuple<T1, T2, T3, T4, T5, T6, T7>)o;
		    return this == tuple;
	    }

        /// <summary>
        ///
        /// </summary>
	    public bool Equals(Tuple<T1, T2, T3, T4, T5, T6, T7> other)
	    {
		    return this == other;
	    }

        /// <summary>
        ///
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj is ATuple<T1, T2, T3, T4, T5, T6, T7> tuple && tuple == this) return 0;
            return 1;
        }

        /// <summary>
        /// 作为默认哈希函数。
        /// </summary>
        /// <returns>当前的哈希代码</returns>
	    public override int GetHashCode()
	    {
		    const int num0 = 17 * 23;
		    var num1 = (num0 + (Item1 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num2 = (num1 + (Item2 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num3 = (num2 + (Item3 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num4 = (num3 + (Item4 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num5 = (num4 + (Item5 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num6 = (num5 + (Item6 != null ? Item1.GetHashCode() : 0)) * 23;
		    var num7 = (num6 + (Item7 != null ? Item1.GetHashCode() : 0));
		    return num7;
	    }

	    public static bool operator ==(ATuple<T1, T2, T3, T4, T5, T6, T7> a, Tuple<T1, T2, T3, T4, T5, T6, T7> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
   		    if (a.Item4 == null && b.Item4 != null) return false;
            if (a.Item4 != null && !a.Item4.Equals(b.Item4)) return false;
   		    if (a.Item5 == null && b.Item5 != null) return false;
            if (a.Item5 != null && !a.Item5.Equals(b.Item5)) return false;
   		    if (a.Item6 == null && b.Item6 != null) return false;
            if (a.Item6 != null && !a.Item6.Equals(b.Item6)) return false;
   		    if (a.Item7 == null && b.Item7 != null) return false;
            if (a.Item7 != null && !a.Item7.Equals(b.Item7)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3, T4, T5, T6, T7> a, Tuple<T1, T2, T3, T4, T5, T6, T7> b)
        {
            return !(a == b);
        }

	    public static bool operator ==(ATuple<T1, T2, T3, T4, T5, T6, T7> a, ATuple<T1, T2, T3, T4, T5, T6, T7> b)
        {
            if ((object)a == null) return (object)b == null;
            if ((object)b == null) return false;
   		    if (a.Item1 == null && b.Item1 != null) return false;
            if (a.Item1 != null && !a.Item1.Equals(b.Item1)) return false;
   		    if (a.Item2 == null && b.Item2 != null) return false;
            if (a.Item2 != null && !a.Item2.Equals(b.Item2)) return false;
   		    if (a.Item3 == null && b.Item3 != null) return false;
            if (a.Item3 != null && !a.Item3.Equals(b.Item3)) return false;
   		    if (a.Item4 == null && b.Item4 != null) return false;
            if (a.Item4 != null && !a.Item4.Equals(b.Item4)) return false;
   		    if (a.Item5 == null && b.Item5 != null) return false;
            if (a.Item5 != null && !a.Item5.Equals(b.Item5)) return false;
   		    if (a.Item6 == null && b.Item6 != null) return false;
            if (a.Item6 != null && !a.Item6.Equals(b.Item6)) return false;
   		    if (a.Item7 == null && b.Item7 != null) return false;
            if (a.Item7 != null && !a.Item7.Equals(b.Item7)) return false;
            return true;
        }

	    public static bool operator !=(ATuple<T1, T2, T3, T4, T5, T6, T7> a, ATuple<T1, T2, T3, T4, T5, T6, T7> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 解包元组。
        /// </summary>
	    public void Unpack(Action<T1, T2, T3, T4, T5, T6, T7> unpackerDelegate)
	    {
		    unpackerDelegate(Item1, Item2, Item3, Item4, Item5, Item6, Item7);
	    }
    }

}