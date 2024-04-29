#region

using System;
using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// IList扩展
    /// </summary>
    partial class ExtendIList
    {
        /// <summary>
        /// 插入
        /// </summary>
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