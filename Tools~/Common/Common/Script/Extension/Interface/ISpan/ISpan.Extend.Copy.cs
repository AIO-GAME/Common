#region

using System;

#endregion

namespace AIO
{
    partial class ExtendISpan
    {
        /// <summary>
        /// 复制
        /// </summary>
        public static void CopyTo<T>(this T[] arrays, out T[] copy, in int length)
        {
            copy = new T[length];
            Array.ConstrainedCopy(arrays, 0, copy, 0, Math.Min(length, arrays.Length));
        }

        /// <summary>
        /// 复制
        /// </summary>
        public static void CopyTo<T>(this T[] arrays, out T[] copy)
        {
            copy = new T[arrays.Length];
            Array.ConstrainedCopy(arrays, 0, copy, 0, arrays.Length);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.CopyTo()
        /// </remarks>
        public static T[] CopyTo<T>(this T[] arrays)
        {
            var copy = new T[arrays.Length];
            Array.ConstrainedCopy(arrays, 0, copy, 0, arrays.Length);
            return copy;
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <remarks>
        /// 并重新分配内存 source = source.CopyTo(3)
        /// </remarks>
        public static T[] CopyTo<T>(this T[] arrays, in int length)
        {
            var copy = new T[length];
            Array.ConstrainedCopy(arrays, 0, copy, 0, Math.Min(length, arrays.Length));
            return copy;
        }

        /// <summary>
        /// 复制
        /// </summary>
        public static void CopyTo<T>(this T[] arrays, T[] copy, in int length)
        {
            if (copy is null) throw new ArgumentNullException(nameof(copy));
            Array.ConstrainedCopy(arrays, 0, copy, 0, Math.Min(length, arrays.Length));
        }

        /// <summary>
        /// 复制
        /// </summary>
        public static void CopyTo<T>(this T[] arrays, T[] copy)
        {
            if (copy is null) throw new ArgumentNullException(nameof(copy));
            Array.ConstrainedCopy(arrays, 0, copy, 0, Math.Min(copy.Length, arrays.Length));
        }
    }
}