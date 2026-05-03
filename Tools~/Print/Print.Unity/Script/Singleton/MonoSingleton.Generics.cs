using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AIO.Internal;
using UnityEngine.Scripting;
using Debug = UnityEngine.Debug;

namespace AIO
{
    /// <summary>
    /// 从这个基类继承以创建单例。
    /// </summary>
    [Preserve]
    public class MonoSingleton<T> : MonoSingleton
    where T : MonoSingleton
    {
        private static Lazy<T> LazyInstance;

        /// <summary>
        /// 是否存在实例
        /// </summary>
        public static bool IsNull
        {
            [DebuggerStepThrough, DebuggerHidden] get => LazyInstance == null || LazyInstance is
            {
                IsValueCreated: false
            };
        }

        /// <summary>
        /// 注册单例
        /// </summary>
        public static void Register()
        {
            if (LazyInstance != null) return;
            lock (@lock) LazyInstance = new Lazy<T>(CreateInstance);
            LazyInstance.Value.RegisterTask.Wait();
        }

        /// <summary>
        /// 注册单例
        /// </summary>
        public static async Task RegisterAsync()
        {
            if (LazyInstance == null)
                lock (@lock)
                    LazyInstance = new Lazy<T>(CreateInstance);

            await LazyInstance.Value.RegisterTask;
        }

        private static T CreateInstance()
        {
            lock (@lock) return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// 单例实例。
        /// </summary>
        public static T Instance
        {
            get
            {
                if (ShuttingDown)
                {
                    Debug.LogWarning($"【Singleton】 Instance '{typeof(T)}' already destroyed. Returning null.");
                    return null;
                }

                if (LazyInstance == null)
                {
                    throw new Exception($"【Singleton】 Instance '{typeof(T)}' is not registered. Please call '{nameof(RegisterAsync)}' first.");
                }

                if (!LazyInstance.Value.IsInitialized)
                {
                    throw new Exception($"【Singleton】 Instance '{typeof(T)}' is not initialized. Please call '{nameof(RegisterAsync)}' first.");
                }

                return LazyInstance.Value;
            }
        }

        /// <inheritdoc />
        [DebuggerStepThrough, DebuggerHidden]
        internal sealed override void Dispose()
        {
            if (!IsInitialized) return;
            OnDispose();
            LazyInstance = null;
            Log?.Dispose();
            IsInitialized = false;
        }
    }
}