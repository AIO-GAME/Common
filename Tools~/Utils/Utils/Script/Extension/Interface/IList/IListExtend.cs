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
        /// 数组 最后一个
        /// </summary>
        public static T Last<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 0) return default;
            return array[array.Count - 1];
        }

        /// <summary>
        /// 数组 第一个
        /// </summary>
        public static T First<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 0) return default;
            return array[0];
        }

        /// <summary>
        /// 获取指定下标元素
        /// </summary>
        public static T Get<T>(this IList<T> array, int idx)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (idx < 0 || idx >= array.Count) throw new IndexOutOfRangeException();
            return array[idx];
        }

        /// <summary>
        /// 设置指定下标元素
        /// </summary>
        public static void Set<T>(this IList<T> array, int idx, T value)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (idx < 0 || idx >= array.Count) throw new IndexOutOfRangeException();
            array[idx] = value;
        }

        #region Remove

        /// <summary>
        /// 移除第一个元素
        /// </summary>
        public static T RemoveFirst<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 0) return default;
            var r = array[0];
            array.RemoveAt(0);
            return r;
        }

        /// <summary>
        /// 移除最后一个元素
        /// </summary>
        public static T RemoveLast<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 0) return default;
            var idx = array.Count - 1;
            var r = array[idx];
            array.RemoveAt(idx);
            return r;
        }

        #endregion

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
        /// 添加
        /// </summary>
        public static void Add<T>(this IList<T> array, params T[] arrys)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arrys) array.Add(item);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void Add<T>(this IList<T> array, in ICollection<T> arrys)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            foreach (var item in arrys) array.Add(item);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public static void AddRange<T>(this IList<T> array, in IList<T> others, int start, int end)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (others == null || others.Count == 0) return;

            start = MathUtils.Clamp(start, 0, others.Count);
            end = MathUtils.Clamp(end, start, others.Count);
            if (start < others.Count - 1 && end > start)
            {
                for (var i = start; i < end; ++i) array.Add(others[i]);
            }
        }

        /// <summary>
        /// 添加相同元素
        /// </summary>
        public static void Union<T>(this IList<T> array, ICollection<T> others)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (others is null || others.Count == 0) return;
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
        public static bool ExistRepeat<T>(this IList<T> array, Func<T, T, bool> compare)
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
        public static bool ExistRepeat<T>(this IList<T> array, IComparer<T> compare)
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
        public static void Swap<T>(this IList<T> array, int A, int B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, short A, short B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, ushort A, ushort B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, byte A, byte B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= A || array.Count <= B) throw new IndexOutOfRangeException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<T>(this IList<T> array, sbyte A, sbyte B)
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
        public static T GetMinValue<T>(this IList<T> array, Func<T, T, sbyte> compare)
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
        public static T GetMinValue<T>(this IList<T> array, IComparer<T> compare)
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
        public static T GetMaxValue<T>(this IList<T> array, IComparer<T> compare)
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
        public static (T, T) GetMaxMinValue<T>(this IList<T> array, IComparer<T> compare)
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
        public static (T, T) GetMaxMinValue<T>(this IList<T> array, Func<T, T, sbyte> compare)
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