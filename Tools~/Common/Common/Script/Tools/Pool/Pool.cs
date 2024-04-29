#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// 对象池
    /// </summary>
    public static partial class Pool
    {
        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.List&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.List&lt;T>"/></returns>
        public static List<T> List<T>()
        {
            return AList<T>.New();
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.HashSet&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.HashSet&lt;T>"/></returns>
        public static HashSet<T> HashSet<T>()
        {
            return AHashSet<T>.New();
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.LinkedList&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.LinkedList&lt;T>"/></returns>
        public static LinkedList<T> LinkedList<T>()
        {
            return ALinkedList<T>.New();
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Stack&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Stack&lt;T>"/></returns>
        public static Stack<T> Stack<T>()
        {
            return AStack<T>.New();
        }

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.Stack&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        public static void Free<T>(Stack<T> list)
        {
            AStack<T>.Free(list);
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Queue&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Queue&lt;T>"/></returns>
        public static Queue<T> Queue<T>()
        {
            return AQueue<T>.New();
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.SortedList&lt;K,V>"/>
        /// </summary>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Dictionary&lt;K,V>"/></returns>
        public static SortedList<K, V> SortedList<K, V>()
        {
            return ASortedList<K, V>.New();
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Dictionary&lt;K,V>"/>
        /// </summary>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Dictionary&lt;K,V>"/></returns>
        public static Dictionary<K, V> Dictionary<K, V>()
        {
            return ADictionary<K, V>.New();
        }

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.List&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        public static void Free<T>(List<T> list)
        {
            AList<T>.Free(list);
        }

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.HashSet&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        public static void Free<T>(HashSet<T> list)
        {
            AHashSet<T>.Free(list);
        }

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.Queue&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        public static void Free<T>(Queue<T> list)
        {
            AQueue<T>.Free(list);
        }

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.SortedList&lt;K,V>"/>
        /// </summary>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        public static void Free<K, V>(SortedList<K, V> list)
        {
            ASortedList<K, V>.Free(list);
        }


        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.Dictionary&lt;K,V>"/>
        /// </summary>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        public static void Free<K, V>(Dictionary<K, V> list)
        {
            ADictionary<K, V>.Free(list);
        }

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.LinkedList&lt;T>"/>
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        public static void Free<T>(LinkedList<T> list)
        {
            ALinkedList<T>.Free(list);
        }
    }
}