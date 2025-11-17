#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AIO.Internal;

#endregion

namespace AIO
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public abstract class Singleton : IDisposable
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        protected internal static readonly object @lock = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        protected internal Singleton()
        {
            RealType     = GetType();
            RegisterTask = InitializeAsync();
        }

        /// <summary>
        /// 完整名称回调
        /// </summary>
        public static Func<Type, string> OnFullName { get; set; }

        private string _fullNameCache;

        /// <summary>
        /// 完整名称
        /// </summary>
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_fullNameCache))
                {
                    _fullNameCache = OnFullName?.Invoke(RealType) ?? RealType.FullName;
                }

                return _fullNameCache;
            }
        }

        /// <summary>
        /// 是否自动释放
        /// </summary>
        protected internal bool AutoDispose { get; set; } = true;

        /// <summary>
        /// 自动释放优先级，数值越大优先级越高
        /// </summary>
        protected internal int AutoDisposePriority { get; set; } = 0;

        /// <summary>
        /// 注册单例
        /// </summary>
        internal Task RegisterTask { get; private set; }

        /// <summary>
        /// 重置单例
        /// </summary>
        public async Task ResetAsync() { await OnResetAsync(); }

        /// <summary>
        /// 重置单例
        /// </summary>
        protected virtual Task OnResetAsync() { return Task.CompletedTask; }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        [DebuggerStepThrough, DebuggerHidden]
        internal virtual void Dispose()
        {
            if (!IsInitialized) return;
            OnDispose();
            IsInitialized = false;
        }

        /// <inheritdoc />
        void IDisposable.Dispose() => Dispose();

        /// <summary>
        /// 单例是否已初始化。
        /// </summary>
        public bool IsInitialized { get; internal set; } = false;

        /// <summary>
        /// 单例的真实类型。
        /// </summary>
        public Type RealType { get; private set; }

        /// <summary>
        /// 初始化单例。
        /// </summary>
        private async Task InitializeAsync()
        {
            if (IsInitialized) return;
            lock (@lock)
            {
                if (this is SingletonArray array)
                {
                    SingletonData.Array.Enqueue(array);
                }
                else
                {
                    if (SingletonData.Data.ContainsKey(RealType))
                    {
                        throw new InvalidOperationException($"单例类型 {RealType.FullName} 已存在，请勿重复初始化同一单例类型。");
                    }

                    SingletonData.Data.Add(RealType, this);
                }
            }

            await OnInitializeAsync();
            IsInitialized = true;
            RegisterTask  = Task.CompletedTask;
        }

        /// <summary>
        /// 初始化单例。
        /// </summary>
        protected virtual Task OnInitializeAsync() => Task.CompletedTask;

        /// <summary>
        /// 确定指定的对象是否等于当前对象。
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象。</param>
        /// <returns>如果指定的对象等于当前对象，则为 true，否则为 false。</returns>
        public sealed override bool Equals(object obj) => false;

        /// <summary>
        /// 作为默认哈希函数。
        /// </summary>
        /// <returns>当前的哈希代码</returns>
        public sealed override int GetHashCode() => 0;

        /// <summary>
        /// 执行与释放或重置非托管资源关联的应用程序定义的任务
        /// </summary>
        protected virtual void OnDispose() { }
    }
}