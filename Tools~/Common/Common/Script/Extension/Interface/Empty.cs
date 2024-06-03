#region

using System.Collections.Generic;
using System.Collections.ObjectModel;

#endregion

namespace AIO
{
    /// <summary>
    /// 提供各种类型的空集合。
    /// </summary>
    /// <typeparam name="T">集合中元素的类型。</typeparam>
    public static class Empty<T>
    {
        /// <summary>
        /// 表示一个空的数组。
        /// </summary>
        public static readonly T[] Array = System.Array.Empty<T>();

        /// <summary>
        /// 表示一个空的 Collection 类型。
        /// </summary>
        public static readonly Collection<T> Collection = new Collection<T>();

        /// <summary>
        /// 表示一个空的 List 类型。
        /// </summary>
        public static readonly List<T> List = new List<T>(0);

        /// <summary>
        /// 表示一个空的 HashSet 类型。
        /// </summary>
        public static readonly HashSet<T> HashSet = new HashSet<T>();

        /// <summary>
        /// 表示一个空的 Queue 类型。
        /// </summary>
        public static readonly Queue<T> Queue = new Queue<T>();

        /// <summary>
        /// 表示一个空的 Stack 类型。
        /// </summary>
        public static readonly Stack<T> Stack = new Stack<T>();

        /// <summary>
        /// 表示一个空的 LinkedList 类型。
        /// </summary>
        public static readonly LinkedList<T> LinkedList = new LinkedList<T>();
    }

    /// <summary>
    /// 提供一种类型为 KeyValuePair&lt;K, V> 的空字典。
    /// </summary>
    /// <typeparam name="K">字典的键类型。</typeparam>
    /// <typeparam name="V">字典的值类型。</typeparam>
    public static class Empty<K, V>
    {
        /// <summary>
        /// 表示一个空的 Dictionary 类型。
        /// </summary>
        public static readonly Dictionary<K, V> Dictionary = new Dictionary<K, V>();
    }
}