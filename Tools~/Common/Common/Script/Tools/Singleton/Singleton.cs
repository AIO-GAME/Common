/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-10
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Diagnostics;

namespace AIO
{
    /// <summary>
    /// 单例模式
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public abstract class Singleton<T> where T : IDisposable, new()
    {
        private static T mInstance;

        /// <summary>
        /// 实例
        /// </summary>
        protected static T Instance
        {
            get
            {
                //如果是引用类型创建一个T实例，如果是值类型返回值的默认值
                if (mInstance == null) mInstance = default;
                if (mInstance == null) mInstance = Activator.CreateInstance<T>();
                return mInstance;
            }
            set { mInstance = value; }
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        public static void CreateInstance()
        {
            if (mInstance == null) mInstance = default;
            if (mInstance == null) mInstance = Activator.CreateInstance<T>();
        }

        /// <summary>
        /// 释放实例
        /// </summary>
        public static void ReleaseInstance()
        {
            mInstance.Dispose();
        }

        /// <summary>
        /// 是否存在实例
        /// </summary>
        public static bool HasInstance
        {
            [DebuggerStepThrough] get { return mInstance != null; }
        }
    }
}