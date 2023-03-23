/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-21                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    /// <summary>
    /// IList扩展
    /// </summary>
    public static partial class IListExtend
    {
        /// <summary>
        /// 插入
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Insert<T>(this IList<T> list, int index, in IEnumerable<T> value)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (list.Count <= index || list.Count == 0)
                throw new IndexOutOfRangeException(nameof(index));
            foreach (var item in value) list.Insert(index++, item);
        }

        /// <summary>
        /// 插入
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Insert<T>(this IList<T> list, int index, in IList<T> value)
        {
            if (list is null) throw new ArgumentNullException(nameof(list));
            if (value is null) throw new ArgumentNullException(nameof(value));
            if (list.Count <= index || list.Count == 0)
                throw new IndexOutOfRangeException(nameof(index));

            for (var i = index; i < value.Count; i++)
                list.Insert(i, value[i]);
        }
    }
}