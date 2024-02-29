using System;
using System.Collections.Generic;

namespace AIO
{
    partial class ExtendIDictionary
    {
        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public static void Swap<K, V>(this IDictionary<K, V> array, in K A, in K B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }
    }
}