/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-21                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;

namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class IListExtend
    {
    
       

        /// <summary>
        /// 保留至少N个元素
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="count">移除数量</param>
        /// <param name="front">Ture:从第一个移除 False:从最后一个移除</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <exception cref="ArgumentNullException">参数为NULL</exception>
        public static void Remain<T>(this IList<T> array, in int count, in bool front = true)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count < 0) return;
            if (array.Count <= count) array.Clear();
            else
            {
                var removeCnt = array.Count - count;
                if (front)
                    for (var i = 0; i < removeCnt; ++i)
                        array.RemoveAt(0);
                else
                    for (var i = 0; i < removeCnt; ++i)
                        array.RemoveAt(count);
            }
        }

       

        /// <summary>
        /// 添加相同元素
        /// </summary>
        public static void Union<T>(this IList<T> array, in IEnumerable<T> others)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (others is null) return;
            foreach (var t in others)
                if (!array.Contains(t))
                    array.Add(t);
        }

        /// <summary>
        /// 是否存在重复的
        /// </summary>
        public static bool ExistRepeat<T>(this IList<T> array) where T : IComparable
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            for (var i = 0; i < array.Count - 1; i++)
            for (var j = i + 1; j < array.Count; j++)
                if (array[i].CompareTo(array[j]) == 0)
                    return true;
            return false;
        }

        /// <summary>
        /// 是否存在重复的
        /// </summary>
        public static bool ExistRepeat<T>(this IList<T> array, in Func<T, T, bool> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            for (var i = 0; i < array.Count - 1; i++)
            for (var j = i + 1; j < array.Count; j++)
                if (compare(array[i], array[j]))
                    return true;
            return false;
        }

        /// <summary>
        /// 是否存在重复的
        /// </summary>
        public static bool ExistRepeat<T>(this IList<T> array, in IComparer<T> compare)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (compare is null) throw new ArgumentNullException(nameof(compare));

            for (var i = 0; i < array.Count - 1; i++)
            for (var j = i + 1; j < array.Count; j++)
                if (compare.Compare(array[i], array[j]) == 0)
                    return true;
            return false;
        }

        #region Swap

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, in int A, in int B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, in short A, in short B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, in ushort A, in ushort B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, in byte A, in byte B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, in sbyte A, in sbyte B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        #endregion

        #region 获取最小值

        /// <summary>
        /// 获取最小值
        /// </summary>
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
        /// 获取最小值
        /// </summary>
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
        /// 获取最小值
        /// </summary>
        /// <returns></returns>
        public static T GetMinValue<T>(this IList<T> array) where T : IComparable
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            var value = array[0];
            foreach (var item in array)
                if (value.CompareTo(item) > 0)
                    value = item;
            return value;
        }

        #endregion

        #region 获取最大值

        /// <summary>
        /// 获取极值 最大值
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="compare">
        /// 对比函数 返回值 0:相等
        /// a大余b情况返回  1:获取最大值
        /// a大余b情况返回 -1:获取最小值
        /// </param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>极值</returns>
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
        /// 获取极值 最大值
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="compare">
        /// 对比函数 返回值 0:相等
        /// a大余b情况返回  1:获取最大值
        /// a大余b情况返回 -1:获取最小值
        /// </param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>极值</returns>
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
        /// 获取极值 最大值
        /// </summary>
        /// <param name="array">数组</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>极值</returns>
        public static T GetMaxValue<T>(this IList<T> array) where T : IComparable
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            var value = array[0];
            foreach (var item in array)
                if (value.CompareTo(item) < 0)
                    value = item;
            return value;
        }

        #endregion

        #region 获取最大最小值

        /// <summary>
        /// 获取最大最小值 第一个Max 第二个Min
        /// </summary>
        public static (T, T) GetMaxMinValue<T>(this IList<T> array) where T : IComparable
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
        /// 获取最大最小值 第一个Max 第二个Min
        /// </summary>
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
        /// 获取最大最小值 第一个Max 第二个Min
        /// </summary>
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