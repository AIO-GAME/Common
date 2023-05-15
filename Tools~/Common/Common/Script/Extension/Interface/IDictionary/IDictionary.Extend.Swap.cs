﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    public static partial class IDictionaryExtend
    {
        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Swap<K, V>(this IDictionary<K, V> array, in K A, in K B)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            (array[A], array[B]) = (array[B], array[A]);
        }
    }
}