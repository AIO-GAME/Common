using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AIO
{
    /// <summary>
    /// 单例模式
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    [DebuggerStepThrough]
    public abstract class Singleton<T> : Singleton
    where T : Singleton<T>
    {
        private static Lazy<T> LazyInstance;

        /// <summary>
        /// 注册单例
        /// </summary>
        public static void Register()
        {
            if (LazyInstance == null) RegisterInternal();
            else LazyInstance.Value.RegisterTask.Wait();
        }

        private static void RegisterInternal()
        {
            lock (@lock) LazyInstance = new Lazy<T>(Activator.CreateInstance<T>);
            LazyInstance.Value.RegisterTask.Wait();
        }

        /// <summary>
        /// 注册单例
        /// </summary>
        public static async Task RegisterAsync()
        {
            if (LazyInstance == null)
            {
                lock (@lock) LazyInstance = new Lazy<T>(Activator.CreateInstance<T>);
            }

            await LazyInstance.Value.RegisterTask;
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static T Inst
        {
            get
            {
                if (LazyInstance == null) RegisterInternal();
                if (!LazyInstance.Value.IsInitialized)
                    throw new Exception($"【Singleton】 Instance '{typeof(T).FullName}' is not initialized. Please call '{nameof(RegisterAsync)}' first.");
                return LazyInstance.Value;
            }
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (LazyInstance == null) RegisterInternal();
                if (!LazyInstance.Value.IsInitialized)
                    throw new Exception($"【Singleton】 Instance '{typeof(T).FullName}' is not initialized. Please call '{nameof(RegisterAsync)}' first.");
                return LazyInstance.Value;
            }
        }

        /// <summary>
        /// 是否存在实例
        /// </summary>
        public static bool IsNull
        {
            [DebuggerStepThrough, DebuggerHidden] get => LazyInstance is
            {
                IsValueCreated: true
            };
        }

        /// <summary>
        /// 获取单例成员
        /// </summary>
        /// <typeparam name="TSingleton"> 泛型 </typeparam>
        /// <returns> 单例成员 </returns>
        protected static TSingleton Member<TSingleton>()
        where TSingleton : Singleton<TSingleton> => Singleton<TSingleton>.Instance;

        /// <inheritdoc />
        internal sealed override void Dispose()
        {
            if (!IsInitialized) return;
            OnDispose();
            LazyInstance  = null;
            IsInitialized = false;
        }
    }
}