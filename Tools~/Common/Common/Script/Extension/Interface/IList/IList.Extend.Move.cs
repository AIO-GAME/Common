using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AIO
{
    partial class ExtendIList
    {
        /// <summary>
        /// 移动项目到列表顶部
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void MoveUp<T>(this IList list, T item)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (item == null) throw new ArgumentNullException(nameof(item));
            var index = list.IndexOf(item);
            if (index <= 0) return;
            list.Remove(item);
            list.Insert(index - 1, item);
        }

        /// <summary>
        /// 移动项目到列表底部
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void MoveDown<T>(this IList list, T item)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            if (item == null) throw new ArgumentNullException(nameof(item));
            var index = list.IndexOf(item);
            if (index < 0 || index >= list.Count - 1) return;
            list.Remove(item);
            list.Insert(index + 1, item);
        }

        /// <summary>
        /// 移动项目到列表顶部
        /// </summary>
        /// <param name="list"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        public static void MoveToTop<T>(this IList list, IEnumerable<T> items)
        {
            var moveList = items.ToArray();                   // Decouple from upstream.
            foreach (var item in moveList) list.Remove(item); // Remove all the existing items
            int index = 0;                                    // Insert all items in order at the top of the list.
            foreach (var item in moveList)
            {
                list.Insert(index, item);
                index += 1;
            }
        }

        /// <summary>
        /// 移动项目到列表底部
        /// </summary>
        /// <param name="list"></param>
        /// <param name="items"></param>
        /// <typeparam name="T"></typeparam>
        public static void MoveToBottom<T>(this IList list, IEnumerable<T> items)
        {
            var moveList = items.ToArray();                   // Decouple from upstream.
            foreach (var item in moveList) list.Remove(item); // Remove all the existing items
            foreach (var item in moveList) list.Add(item);    // Insert all items in order at the top of the list.
        }
    }
}