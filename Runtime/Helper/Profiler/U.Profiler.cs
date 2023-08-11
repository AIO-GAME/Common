using UnityEngine.Profiling;

namespace AIO
{
    public static partial class RHelper
    {
        /// <summary>
        /// 分析器
        /// </summary>
        public static partial class ProfilerX
        {
            /// <summary>
            /// 获取当前数据 在Unity内部声明内存
            /// </summary>
            /// <param name="obj">数据</param>
            /// <returns>占用内存</returns>
            public static long GetMemoryObject<T>(T obj) where T : UnityEngine.Object
            {
                return Profiler.GetRuntimeMemorySizeLong(obj);
            }
        }
    }
}
