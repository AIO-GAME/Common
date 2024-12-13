#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 获取 集合 指定下标
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IList<T> array, in int index)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (index < 0 || index >= array.Count) throw new IndexOutOfRangeException();
            return array[index];
        }

        /// <summary>
        /// 获取 集合 最后一个
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetLast<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            return array.Count <= 0 ? default : array[array.Count - 1];
        }

        /// <summary>
        /// 获取 集合 第一个
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetFirst<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            return array.Count <= 0 ? default : array[0];
        }

        #region 获取最小值

        /// <summary>
        /// 获取 集合 最小值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinValue<T>(this IList<T> array, in Func<T, T, sbyte> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            var value = array[0];
            foreach (var item in array)
                if (compare(value, item) > 0)
                    value = item;
            return value;
        }

        /// <summary>
        /// 获取 集合 最小值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinValue<T>(this IList<T> array, in IComparer<T> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            var value = array[0];
            foreach (var item in array)
                if (compare.Compare(value, item) > 0)
                    value = item;
            return value;
        }

        /// <summary>
        /// 获取 集合 最小值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMinValue<T>(this IList<T> array)
        where T : IComparable<T>
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            var value                                                                  = array[0];
            foreach (var item in array.Where(item => value.CompareTo(item) > 0)) value = item;
            return value;
        }

        #endregion

        #region 获取最大值

        /// <summary>
        /// 获取 集合极值 最大值
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="compare">
        /// 对比函数 返回值 0:相等
        /// a大余b情况返回  1:获取最大值
        /// a大余b情况返回 -1:获取最小值
        /// </param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>极值</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxValue<T>(this IList<T> array, in Func<T, T, sbyte> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            var value = array[0];
            foreach (var item in array)
                if (compare(value, item) < 0)
                    value = item;
            return value;
        }

        /// <summary>
        /// 获取 集合极值 最大值
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="compare">
        /// 对比函数 返回值 0:相等
        /// a大余b情况返回  1:获取最大值
        /// a大余b情况返回 -1:获取最小值
        /// </param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>极值</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxValue<T>(this IList<T> array, in IComparer<T> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            var value = array[0];
            foreach (var item in array)
                if (compare.Compare(value, item) < 0)
                    value = item;
            return value;
        }

        /// <summary>
        /// 获取 集合极值 最大值
        /// </summary>
        /// <param name="array">数组</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>极值</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetMaxValue<T>(this IList<T> array)
        where T : IComparable<T>
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            var value                                                                  = array[0];
            foreach (var item in array.Where(item => value.CompareTo(item) < 0)) value = item;
            return value;
        }

        #endregion

        #region 获取最大最小值

        /// <summary>
        /// 获取 集合最大最小值 第一个Max 第二个Min
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T, T) GetMaxMinValue<T>(this IList<T> array)
        where T : IComparable
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            var max = array[0];
            var min = array[0];

            foreach (var item in array)
            {
                if (item.CompareTo(max) > 0) max = item;
                if (item.CompareTo(min) < 0) min = item;
            }

            return (max, min);
        }

        /// <summary>
        /// 获取 集合最大最小值 第一个Max 第二个Min
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T, T) GetMaxMinValue<T>(this IList<T> array, in IComparer<T> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            var max = array[0];
            var min = array[0];

            foreach (var item in array)
            {
                if (compare.Compare(item, max) > 0) max = item;
                if (compare.Compare(item, min) < 0) min = item;
            }

            return (max, min);
        }

        /// <summary>
        /// 获取 集合最大最小值 第一个Max 第二个Min
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (T, T) GetMaxMinValue<T>(this IList<T> array, in Func<T, T, sbyte> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            var max = array[0];
            var min = array[0];

            foreach (var item in array)
            {
                if (compare(item, max) > 0) max = item;
                if (compare(item, min) < 0) min = item;
            }

            return (max, min);
        }

        #endregion
    }
}
