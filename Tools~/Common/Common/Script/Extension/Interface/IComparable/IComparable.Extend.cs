#region

using System;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    /// <summary>
    /// 扩展 ->
    /// 定义由值类型或类实现的通用比较方法，以为排序实例创建类型特定的比较方法。
    /// </summary>
    public static class ExtendIComparable
    {
        /// <summary>
        /// 如果第一个对象 小于 第二个对象 则返回 true。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLT<T>(this IComparable<T> x, in T y)
        {
            return x.CompareTo(y) < 0;
        }

        /// <summary>
        /// 如果第一个对象 等于 第二个对象，则返回 true。
        /// </summary>                                                              
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEQ<T>(this IComparable<T> x, in T y)
        {
            return x.CompareTo(y) == 0;
        }

        /// <summary>
        /// 如果第一个对象 大于 第二个对象，则返回 true。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGT<T>(this IComparable<T> x, in T y)
        {
            return x.CompareTo(y) > 0;
        }

        /// <summary>
        /// 如果第一个对象 不等于 第二个对象，则返回 true。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNE<T>(this IComparable<T> x, in T y)
        {
            return x.CompareTo(y) != 0;
        }

        /// <summary>
        /// 如果第一个对象 小于或等于 第二个对象，则返回 true。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLE<T>(this IComparable<T> x, in T y)
        {
            return x.CompareTo(y) <= 0;
        }

        /// <summary>
        /// 如果第一个对象 大于或等于 第二个对象，则返回 true。
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGE<T>(this IComparable<T> x, in T y)
        {
            return x.CompareTo(y) >= 0;
        }
    }
}