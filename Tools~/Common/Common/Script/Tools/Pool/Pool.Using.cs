namespace AIO
{
    using System;
    using System.Collections.Generic;

    public partial class Pool
    {
        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(out T value) where T : class, IDisposable, new()
        {
            var disposable = new Disposable<T>(Activator.CreateInstance<T>(),
                release => { release.Dispose(); });
            value = disposable.Item;
            return disposable;
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(out T value, Action<T> onRelease) where T : class, new()
        {
            var disposable = new Disposable<T>(Activator.CreateInstance<T>(), onRelease);
            value = disposable.Item;
            return disposable;
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(T value, Action<T> onRelease)
        {
            return new Disposable<T>(value, onRelease);
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(out List<T> value)
        {
            var disposable = new Disposable<List<T>>(
                AList<T>.New(),
                release => { release.Free(); });
            value = disposable.Item;
            return disposable;
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(out HashSet<T> value)
        {
            var disposable = new Disposable<HashSet<T>>(
                AHashSet<T>.New(),
                release => { release.Free(); });
            value = disposable.Item;
            return disposable;
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(out Stack<T> value)
        {
            var disposable = new Disposable<Stack<T>>(
                AStack<T>.New(),
                release => { release.Free(); });
            value = disposable.Item;
            return disposable;
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<T>(out Queue<T> value)
        {
            var disposable = new Disposable<Queue<T>>(
                AQueue<T>.New(),
                release => { release.Free(); });
            value = disposable.Item;
            return disposable;
        }

        /// <summary>
        /// 引用释放
        /// </summary>
        public static IDisposable Using<K, V>(out Dictionary<K, V> value)
        {
            var disposable = new Disposable<Dictionary<K, V>>(
                ADictionary<K, V>.New(),
                release => { release.Free(); });
            value = disposable.Item;
            return disposable;
        }
    }
}