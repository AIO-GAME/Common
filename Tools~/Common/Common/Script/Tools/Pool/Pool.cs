#region

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#endregion

namespace AIO
{
    internal static partial class Pooling
    {
        internal static readonly object @lock = new object();
    }

    /// <summary>
    /// 对象池
    /// </summary>
    public static partial class Pool
    {
        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.List{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.List{T}"/></returns>
        public static List<T> List<T>(IEnumerable<T> val)
        {
            var array = Pooling.List<T>.Alloc();
            if (val == null) return array;
            array.AddRange(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.List{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.List{T}"/></returns>
        public static List<T> List<T>(T val)
        {
            var array = Pooling.List<T>.Alloc();
            if (val == null) return array;
            array.Add(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Queue{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Queue{T}"/></returns>
        public static Queue<T> Queue<T>(IEnumerable<T> val)
        {
            var array = Pooling.Queue<T>.Alloc();
            if (val == null) return array;
            foreach (var item in val) array.Enqueue(item);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Stack{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Stack{T}"/></returns>
        public static Stack<T> Stack<T>(IEnumerable<T> val)
        {
            var array = Pooling.Stack<T>.Alloc();
            if (val == null) return array;
            foreach (var item in val) array.Push(item);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.HashSet{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.HashSet{T}"/></returns>
        public static HashSet<T> HashSet<T>(IEnumerable<T> val)
        {
            var array = Pooling.HashSet<T>.Alloc();
            if (val == null) return array;
            foreach (var item in val) array.Add(item);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.LinkedList{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.LinkedList{T}"/></returns>
        public static LinkedList<T> LinkedList<T>(IEnumerable<T> val)
        {
            var array = Pooling.LinkedList<T>.Alloc();
            if (val == null) return array;
            foreach (var item in val) array.AddLast(item);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/></returns>
        public static ConcurrentDictionary<K, V> ConcurrentDictionary<K, V>(IDictionary<K, V> val)
        {
            var array = Pooling.ConcurrentDictionary<K, V>.Alloc();
            if (val == null) return array;
            foreach (var kv in val) array.TryAdd(kv.Key, kv.Value);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentBag{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentBag{T}"/></returns>
        public static ConcurrentBag<T> ConcurrentBag<T>(IEnumerable<T> val)
        {
            var array = Pooling.ConcurrentBag<T>.Alloc();
            if (val == null) return array;
            foreach (var kv in val) array.Add(kv);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/></returns>
        public static ConcurrentQueue<T> ConcurrentQueue<T>(IEnumerable<T> val)
        {
            var array = Pooling.ConcurrentQueue<T>.Alloc();
            if (val == null) return array;
            foreach (var kv in val) array.Enqueue(kv);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentStack{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentStack{T}"/></returns>
        public static ConcurrentStack<T> ConcurrentStack<T>(IEnumerable<T> val)
        {
            var array = Pooling.ConcurrentStack<T>.Alloc();
            if (val == null) return array;
            foreach (var kv in val) array.Push(kv);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Dictionary{K,V}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Dictionary{K,V}"/></returns>
        public static Dictionary<K, V> Dictionary<K, V>(IDictionary<K, V> val)
        {
            var array = Pooling.Dictionary<K, V>.Alloc();
            if (val == null) return array;
            foreach (var kv in val) array.Add(kv.Key, kv.Value);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.SortedList{K,V}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.SortedList{K,V}"/></returns>
        public static SortedList<K, V> SortedList<K, V>(IDictionary<K, V> val)
        {
            var array = Pooling.SortedList<K, V>.Alloc();
            if (val == null) return array;
            foreach (var kv in val) array.Add(kv.Key, kv.Value);
            return array;
        }

        #region MyRegion

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Queue{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Queue{T}"/></returns>
        public static Queue<T> Queue<T>(T val)
        {
            var array = Pooling.Queue<T>.Alloc();
            if (val == null) return array;
            array.Enqueue(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Stack{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Stack{T}"/></returns>
        public static Stack<T> Stack<T>(T val)
        {
            var array = Pooling.Stack<T>.Alloc();
            if (val == null) return array;
            array.Push(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.HashSet{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.HashSet{T}"/></returns>
        public static HashSet<T> HashSet<T>(T val)
        {
            var array = Pooling.HashSet<T>.Alloc();
            if (val == null) return array;
            array.Add(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.LinkedList{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.LinkedList{T}"/></returns>
        public static LinkedList<T> LinkedList<T>(T val)
        {
            var array = Pooling.LinkedList<T>.Alloc();
            if (val == null) return array;
            array.AddLast(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/></returns>
        public static ConcurrentDictionary<K, V> ConcurrentDictionary<K, V>(KeyValuePair<K, V> val)
        {
            var array = Pooling.ConcurrentDictionary<K, V>.Alloc();
            array.TryAdd(val.Key, val.Value);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentBag{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentBag{T}"/></returns>
        public static ConcurrentBag<T> ConcurrentBag<T>(T val)
        {
            var array = Pooling.ConcurrentBag<T>.Alloc();
            if (val == null) return array;
            array.Add(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/></returns>
        public static ConcurrentQueue<T> ConcurrentQueue<T>(T val)
        {
            var array = Pooling.ConcurrentQueue<T>.Alloc();
            if (val == null) return array;
            array.Enqueue(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentStack{T}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentStack{T}"/></returns>
        public static ConcurrentStack<T> ConcurrentStack<T>(T val)
        {
            var array = Pooling.ConcurrentStack<T>.Alloc();
            array.Push(val);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Dictionary{K,V}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Dictionary{K,V}"/></returns>
        public static Dictionary<K, V> Dictionary<K, V>(KeyValuePair<K, V> val)
        {
            var array = Pooling.Dictionary<K, V>.Alloc();
            array.Add(val.Key, val.Value);
            return array;
        }

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.SortedList{K,V}"/>
        /// </summary>
        /// <param name="val">参数</param>
        /// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.SortedList{K,V}"/></returns>
        public static SortedList<K, V> SortedList<K, V>(KeyValuePair<K, V> val)
        {
            var array = Pooling.SortedList<K, V>.Alloc();
            array.Add(val.Key, val.Value);
            return array;
        }

        #endregion

        /// <summary>
        /// 创建通用对象
        /// </summary>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="IPoolable"/></returns>
        public static T Alloc<T>()
        where T : IPoolable, new() => Pooling.Generic<T>.Alloc();

        /// <summary>
        /// 创建通用对象
        /// </summary>
        /// <param name="constructor">构造函数</param>
        /// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="IPoolable"/></returns>
        public static T Alloc<T>(Func<T> constructor)
        where T : IPoolable => Pooling.Generic<T>.Alloc(constructor);

        /// <summary>
        /// 回收通用对象
        /// </summary>
        /// <param name="item"> 对象 </param>
        /// <typeparam name="T">Generic</typeparam>
        public static void Free<T>(this T item)
        where T : IPoolable => Pooling.Generic<T>.Recycle(item);

        /// <summary>
        /// 释放所有对象池
        /// </summary>
        public static void DisposeAll()
        {
            lock (Pooling.@lock)
            {
                while (SingletonData.Array.Count > 0)
                {
                    if (!SingletonData.Array.TryDequeue(out var array)) continue;
                    array?.Dispose();
                    array = null;
                }
            }
        }

        /// <summary>
        /// 清理所有缓存
        /// </summary>
        public static void ClearAllCache()
        {
            lock (Pooling.@lock)
            {
                foreach (var array in SingletonData.Array)
                {
                    array?.ClearCache();
                }
            }
        }
    }
}