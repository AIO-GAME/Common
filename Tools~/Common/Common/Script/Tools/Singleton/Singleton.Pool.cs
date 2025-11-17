using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIO.Internal;

namespace AIO.Internal
{
    /// <summary>
    /// 数组单例基类
    /// </summary>
    public abstract class SingletonArray : Singleton
    {
        /// <summary>
        /// 清空所有对象池
        /// </summary>
        protected abstract void ClearAll();

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        public sealed override string ToString() { return string.Empty; }

        /// <summary>
        /// 总数量
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// 空闲数量
        /// </summary>
        public abstract int FreeCount { get; }

        /// <summary>
        /// 使用中数量
        /// </summary>
        public abstract int BusyCount { get; }

        /// <summary>
        /// 清空缓存对象池
        /// </summary>
        public abstract void ClearCache();
    }
}

namespace AIO
{
    /// <summary>
    /// 数组单例模式
    /// </summary>
    /// <typeparam name="TSingleton"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class SingletonArray<TSingleton, T2> : SingletonArray
    where TSingleton : SingletonArray<TSingleton, T2>
    where T2 : IEnumerable
    {
        private static Lazy<TSingleton> LazyInstance;

        /// <summary>
        /// 空闲集合
        /// </summary>
        protected Lazy<Stack<T2>> free { get; private set; }

        /// <summary>
        /// 使用中集合
        /// </summary>
        protected Lazy<HashSet<T2>> busy { get; private set; }

        /// <inheritdoc />
        protected sealed override Task OnInitializeAsync()
        {
            free                = new Lazy<Stack<T2>>(() => new Stack<T2>());
            busy                = new Lazy<HashSet<T2>>(() => new HashSet<T2>());
            AutoDisposePriority = int.MinValue;
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        internal sealed override void Dispose()
        {
            if (!IsInitialized) return;
            OnDispose();
            LazyInstance = null;
            lock (@lock) ClearAll();

            busy.Value.Clear();
            free.Value.Clear();

            busy          = null;
            free          = null;
            IsInitialized = false;
        }

        /// <summary>
        /// 创建新的
        /// </summary>
        /// <returns></returns>
        protected abstract T2 Create();

        /// <summary>
        /// 回收数组
        /// </summary>
        /// <param name="array"></param>
        protected abstract void Recycle(T2 array);

        /// <summary>
        /// 创建新的
        /// </summary>
        protected T2 Pop()
        {
            lock (@lock)
            {
                if (free.Value.Count == 0) free.Value.Push(Create());
                var array = free.Value.Pop();
                busy.Value.Add(array);
                return array;
            }
        }

        /// <summary>
        /// 释放List
        /// </summary>
        protected void Push(T2 array)
        {
            lock (@lock)
            {
                Recycle(array);
                if (busy.Value.Contains(array)) busy.Value.Remove(array);
                free.Value.Push(array);
            }
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static TSingleton Instance
        {
            get
            {
                if (LazyInstance == null) Register1();
                if (!LazyInstance.Value.IsInitialized)
                    throw new Exception($"【Singleton】 Instance '{typeof(TSingleton)}' is not initialized. Please call '{nameof(Register)}' first.");
                return LazyInstance.Value;
            }
        }

        /// <summary>
        /// 注册单例
        /// </summary>
        public static void Register()
        {
            if (LazyInstance == null) Register1();
            else LazyInstance.Value.RegisterTask.Wait();
        }

        /// <summary>
        /// 注册单例
        /// </summary>
        private static void Register1()
        {
            lock (@lock) LazyInstance = new Lazy<TSingleton>(Activator.CreateInstance<TSingleton>);
            LazyInstance.Value.RegisterTask.Wait();
        }

        /// <summary>
        /// 总数量
        /// </summary>
        public sealed override int Count => busy.Value.Count + free.Value.Count;

        /// <summary>
        /// 空闲数量
        /// </summary>
        public sealed override int FreeCount => free.Value.Count;

        /// <summary>
        /// 使用中数量
        /// </summary>
        public sealed override int BusyCount => busy.Value.Count;

        /// <summary>
        /// 申请
        /// </summary>
        /// <returns> <see cref="T2"/> </returns>
        public static T2 Alloc() => Instance.Pop();

        /// <summary>
        /// 释放
        /// </summary>
        public static void Release(T2 array) { Instance.Push(array); }

        /// <summary>
        /// 清空缓存对象池
        /// </summary>
        public sealed override void ClearCache() { free.Value.Clear(); }
    }
}