/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;

namespace AIO
{
    public static partial class IListExtend
    {
        /// <summary>
        /// 获取指定下标元素
        /// </summary>
        public static T Get<T>(this IList<T> array, in int index)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (index < 0 || index >= array.Count) throw new IndexOutOfRangeException();
            return array[index];
        }

        /// <summary>
        /// 数组 最后一个
        /// </summary>
        public static T GetLast<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 0) return default;
            return array[array.Count - 1];
        }

        /// <summary>
        /// 数组 第一个
        /// </summary>
        public static T GetFirst<T>(this IList<T> array)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (array.Count <= 0) return default;
            return array[0];
        }
    }
}