#region

using System;
using System.Diagnostics;

#endregion

namespace AIO
{
    /// <summary>
    /// 单例模式
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    [DebuggerStepThrough]
    public abstract class Singleton<T> : IDisposable
    where T : IDisposable, new()
    {
        private static Lazy<T> mInstance;

        /// <summary>
        /// 实例
        /// </summary>
        public static T Inst
        {
            get
            {
                if (mInstance == null) mInstance = new Lazy<T>(CreateInstance);
                return mInstance.Value;
            }
        }

        /// <summary>
        /// 是否存在实例
        /// </summary>
        public static bool HasInstance
        {
            [DebuggerStepThrough] get => mInstance != null;
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        [DebuggerStepThrough]
        private static T CreateInstance()
        {
            T inst;
            try
            {
                inst = new T();
            }
            catch
            {
                inst = Activator.CreateInstance<T>();
            }

            return inst;
        }

        [DebuggerStepThrough]
        void IDisposable.Dispose()
        {
            mInstance?.Value.Dispose();
        }
    }
}