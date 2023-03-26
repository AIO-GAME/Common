using System;
using System.Reflection;

/// <summary>
/// 工具类
/// </summary>
public static partial class Utils
{
}

namespace AIO
{

    /// <summary>
    /// 工具类属性
    /// 如果使用该属性 请实现静态函数
    /// Initialize
    /// Dispose
    /// </summary>
    internal class UtilsAttribute : Attribute
    {
        internal const BindingFlags FLAGS = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        public readonly string Initialize;
        public readonly string Dispose;

        public UtilsAttribute(in string initialize, in string dispose)
        {
            Dispose = dispose;
            Initialize = initialize;
        }

        public UtilsAttribute()
        {
            Dispose = nameof(Dispose);
            Initialize = nameof(Initialize);
        }

        /// <summary>
        /// 实现
        /// </summary>
        public void InvokeInitialize(Type typeName)
        {
            var method = typeName.GetMethod(Initialize, FLAGS);
            if (method == null)
            {
                Console.WriteLine(" {0} -> {1} {2} = null", typeName.FullName, Initialize, FLAGS);
            }
            else method.Invoke(null, null);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void InvokeDispose(Type typeName)
        {
            var method = typeName.GetMethod(Dispose, FLAGS);
            if (method == null)
            {
                Console.WriteLine(" {0} -> {1} {2} = null", typeName.FullName, Dispose, FLAGS);
            }
            else method.Invoke(null, null);
        }
    }


    internal static class UtilsManager
    {
        static UtilsManager()
        {
        }
    }
}