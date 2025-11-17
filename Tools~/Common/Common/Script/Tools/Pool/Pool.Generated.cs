using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AIO
{
    partial class Pool
    {

	    #region System.Collections.Generic.List<T>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.List{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.List{T}"/></returns>
        public static System.Collections.Generic.List<T> List<T>() => Pooling.List<T>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.List{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Generic.List{T}"/></param>
        public static void Free<T>(this System.Collections.Generic.List<T> array)
        {
            Pooling.List<T>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Generic.List{T}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Generic.List{T}"/></param>
        public static IDisposable Using<T>(out System.Collections.Generic.List<T> value)
        {
            var disposable = new Disposable<System.Collections.Generic.List<T>>(Pooling.List<T>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Generic.HashSet<T>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.HashSet{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.HashSet{T}"/></returns>
        public static System.Collections.Generic.HashSet<T> HashSet<T>() => Pooling.HashSet<T>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.HashSet{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Generic.HashSet{T}"/></param>
        public static void Free<T>(this System.Collections.Generic.HashSet<T> array)
        {
            Pooling.HashSet<T>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Generic.HashSet{T}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Generic.HashSet{T}"/></param>
        public static IDisposable Using<T>(out System.Collections.Generic.HashSet<T> value)
        {
            var disposable = new Disposable<System.Collections.Generic.HashSet<T>>(Pooling.HashSet<T>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Generic.LinkedList<T>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.LinkedList{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.LinkedList{T}"/></returns>
        public static System.Collections.Generic.LinkedList<T> LinkedList<T>() => Pooling.LinkedList<T>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.LinkedList{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Generic.LinkedList{T}"/></param>
        public static void Free<T>(this System.Collections.Generic.LinkedList<T> array)
        {
            Pooling.LinkedList<T>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Generic.LinkedList{T}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Generic.LinkedList{T}"/></param>
        public static IDisposable Using<T>(out System.Collections.Generic.LinkedList<T> value)
        {
            var disposable = new Disposable<System.Collections.Generic.LinkedList<T>>(Pooling.LinkedList<T>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Generic.Stack<T>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Stack{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Stack{T}"/></returns>
        public static System.Collections.Generic.Stack<T> Stack<T>() => Pooling.Stack<T>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.Stack{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Generic.Stack{T}"/></param>
        public static void Free<T>(this System.Collections.Generic.Stack<T> array)
        {
            Pooling.Stack<T>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Generic.Stack{T}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Generic.Stack{T}"/></param>
        public static IDisposable Using<T>(out System.Collections.Generic.Stack<T> value)
        {
            var disposable = new Disposable<System.Collections.Generic.Stack<T>>(Pooling.Stack<T>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Generic.Queue<T>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Queue{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Queue{T}"/></returns>
        public static System.Collections.Generic.Queue<T> Queue<T>() => Pooling.Queue<T>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.Queue{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Generic.Queue{T}"/></param>
        public static void Free<T>(this System.Collections.Generic.Queue<T> array)
        {
            Pooling.Queue<T>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Generic.Queue{T}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Generic.Queue{T}"/></param>
        public static IDisposable Using<T>(out System.Collections.Generic.Queue<T> value)
        {
            var disposable = new Disposable<System.Collections.Generic.Queue<T>>(Pooling.Queue<T>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Generic.SortedList<K,V>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.SortedList{K,V}"/>
        /// </summary>/// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.SortedList{K,V}"/></returns>
        public static System.Collections.Generic.SortedList<K,V> SortedList<K,V>() => Pooling.SortedList<K,V>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.SortedList{K,V}"/>
        /// </summary>/// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Generic.SortedList{K,V}"/></param>
        public static void Free<K,V>(this System.Collections.Generic.SortedList<K,V> array)
        {
            Pooling.SortedList<K,V>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Generic.SortedList{K,V}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Generic.SortedList{K,V}"/></param>
        public static IDisposable Using<K,V>(out System.Collections.Generic.SortedList<K,V> value)
        {
            var disposable = new Disposable<System.Collections.Generic.SortedList<K,V>>(Pooling.SortedList<K,V>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Generic.Dictionary<K,V>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Generic.Dictionary{K,V}"/>
        /// </summary>/// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Generic.Dictionary{K,V}"/></returns>
        public static System.Collections.Generic.Dictionary<K,V> Dictionary<K,V>() => Pooling.Dictionary<K,V>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.Dictionary{K,V}"/>
        /// </summary>/// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Generic.Dictionary{K,V}"/></param>
        public static void Free<K,V>(this System.Collections.Generic.Dictionary<K,V> array)
        {
            Pooling.Dictionary<K,V>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Generic.Dictionary{K,V}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Generic.Dictionary{K,V}"/></param>
        public static IDisposable Using<K,V>(out System.Collections.Generic.Dictionary<K,V> value)
        {
            var disposable = new Disposable<System.Collections.Generic.Dictionary<K,V>>(Pooling.Dictionary<K,V>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Concurrent.ConcurrentDictionary<K,V>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/>
        /// </summary>/// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/></returns>
        public static System.Collections.Concurrent.ConcurrentDictionary<K,V> ConcurrentDictionary<K,V>() => Pooling.ConcurrentDictionary<K,V>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/>
        /// </summary>/// <typeparam name="K">Generic</typeparam>
        /// <typeparam name="V">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/></param>
        public static void Free<K,V>(this System.Collections.Concurrent.ConcurrentDictionary<K,V> array)
        {
            Pooling.ConcurrentDictionary<K,V>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Concurrent.ConcurrentDictionary{K,V}"/></param>
        public static IDisposable Using<K,V>(out System.Collections.Concurrent.ConcurrentDictionary<K,V> value)
        {
            var disposable = new Disposable<System.Collections.Concurrent.ConcurrentDictionary<K,V>>(Pooling.ConcurrentDictionary<K,V>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Concurrent.ConcurrentBag<T>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentBag{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentBag{T}"/></returns>
        public static System.Collections.Concurrent.ConcurrentBag<T> ConcurrentBag<T>() => Pooling.ConcurrentBag<T>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Concurrent.ConcurrentBag{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Concurrent.ConcurrentBag{T}"/></param>
        public static void Free<T>(this System.Collections.Concurrent.ConcurrentBag<T> array)
        {
            Pooling.ConcurrentBag<T>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Concurrent.ConcurrentBag{T}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Concurrent.ConcurrentBag{T}"/></param>
        public static IDisposable Using<T>(out System.Collections.Concurrent.ConcurrentBag<T> value)
        {
            var disposable = new Disposable<System.Collections.Concurrent.ConcurrentBag<T>>(Pooling.ConcurrentBag<T>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Concurrent.ConcurrentQueue<T>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/></returns>
        public static System.Collections.Concurrent.ConcurrentQueue<T> ConcurrentQueue<T>() => Pooling.ConcurrentQueue<T>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/></param>
        public static void Free<T>(this System.Collections.Concurrent.ConcurrentQueue<T> array)
        {
            Pooling.ConcurrentQueue<T>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Concurrent.ConcurrentQueue{T}"/></param>
        public static IDisposable Using<T>(out System.Collections.Concurrent.ConcurrentQueue<T> value)
        {
            var disposable = new Disposable<System.Collections.Concurrent.ConcurrentQueue<T>>(Pooling.ConcurrentQueue<T>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Concurrent.ConcurrentStack<T>

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Concurrent.ConcurrentStack{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <returns><see cref="System.Collections.Concurrent.ConcurrentStack{T}"/></returns>
        public static System.Collections.Concurrent.ConcurrentStack<T> ConcurrentStack<T>() => Pooling.ConcurrentStack<T>.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Concurrent.ConcurrentStack{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Concurrent.ConcurrentStack{T}"/></param>
        public static void Free<T>(this System.Collections.Concurrent.ConcurrentStack<T> array)
        {
            Pooling.ConcurrentStack<T>.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Concurrent.ConcurrentStack{T}"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Concurrent.ConcurrentStack{T}"/></param>
        public static IDisposable Using<T>(out System.Collections.Concurrent.ConcurrentStack<T> value)
        {
            var disposable = new Disposable<System.Collections.Concurrent.ConcurrentStack<T>>(Pooling.ConcurrentStack<T>.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.SortedList

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.SortedList"/>
        /// </summary>
        /// <returns><see cref="System.Collections.SortedList"/></returns>
        public static System.Collections.SortedList SortedList() => Pooling.SortedList.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.SortedList"/>
        /// </summary>
        /// <param name="array"><see cref="System.Collections.SortedList"/></param>
        public static void Free(this System.Collections.SortedList array)
        {
            Pooling.SortedList.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.SortedList"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.SortedList"/></param>
        public static IDisposable Using(out System.Collections.SortedList value)
        {
            var disposable = new Disposable<System.Collections.SortedList>(Pooling.SortedList.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

	    #region System.Collections.Hashtable

        /// <summary>
        /// 缓存 => 创建 <see cref="System.Collections.Hashtable"/>
        /// </summary>
        /// <returns><see cref="System.Collections.Hashtable"/></returns>
        public static System.Collections.Hashtable Hashtable() => Pooling.Hashtable.Alloc();

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Hashtable"/>
        /// </summary>
        /// <param name="array"><see cref="System.Collections.Hashtable"/></param>
        public static void Free(this System.Collections.Hashtable array)
        {
            Pooling.Hashtable.Release(array);
            array = null;
        }

        /// <summary>
        /// 引用缓存 => 释放 <see cref="System.Collections.Hashtable"/>
        /// </summary>
        /// <param name="value"><see cref="System.Collections.Hashtable"/></param>
        public static IDisposable Using(out System.Collections.Hashtable value)
        {
            var disposable = new Disposable<System.Collections.Hashtable>(Pooling.Hashtable.Alloc(), release => release.Free());
            value = disposable.Item;
            return disposable;
        }

	    #endregion

        /// <summary>
        /// 缓存 => 释放 <see cref="System.Collections.Generic.IEnumerable{T}"/>
        /// </summary>/// <typeparam name="T">Generic</typeparam>
        /// <param name="array"><see cref="System.Collections.Generic.IEnumerable{T}"/></param>
        public static void Free<T>(this IEnumerable<T> array)
        {
            if (array == null) return;
            switch (array)
            {
                case System.Collections.Generic.List<T> list:
                    Pooling.List<T>.Release(list);
                    return;
                case System.Collections.Generic.HashSet<T> hashset:
                    Pooling.HashSet<T>.Release(hashset);
                    return;
                case System.Collections.Generic.LinkedList<T> linkedlist:
                    Pooling.LinkedList<T>.Release(linkedlist);
                    return;
                case System.Collections.Generic.Stack<T> stack:
                    Pooling.Stack<T>.Release(stack);
                    return;
                case System.Collections.Generic.Queue<T> queue:
                    Pooling.Queue<T>.Release(queue);
                    return;
                case System.Collections.Concurrent.ConcurrentBag<T> concurrentbag:
                    Pooling.ConcurrentBag<T>.Release(concurrentbag);
                    return;
                case System.Collections.Concurrent.ConcurrentQueue<T> concurrentqueue:
                    Pooling.ConcurrentQueue<T>.Release(concurrentqueue);
                    return;
                case System.Collections.Concurrent.ConcurrentStack<T> concurrentstack:
                    Pooling.ConcurrentStack<T>.Release(concurrentstack);
                    return;
            }
            array = null;
        }
    }
}