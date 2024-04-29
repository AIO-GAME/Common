#region

using System;

#endregion

namespace AIO
{
    partial class ExtendSort
    {
        /// <summary>
        ///     数组排序
        /// </summary>
        /// <param name="array">数组</param>
        /// <returns>排序后的数组 可无需赋值</returns>
        /// <remarks>支持快速排序、冒泡排序、双向冒泡排序、选择排序、插入排序、希尔排序、堆排序、归并排序、桶排序、基数排序</remarks>
        /// <exception cref="ArgumentOutOfRangeException">传入的排序方式不在枚举内</exception>
        /// <exception cref="ArgumentNullException">传入的数组为空</exception>
        public static T[] Sort<T>(this T[] array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Length <= 1) return array;
            Array.Sort(array);
            return array;
        }
    }
}