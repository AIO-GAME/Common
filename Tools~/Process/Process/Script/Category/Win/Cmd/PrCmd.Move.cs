#region

using System.IO;

#endregion

namespace AIO
{
    partial class PrCmd
    {
        /// <summary>
        /// 移动或重命名 目录
        /// </summary>
        public static class Move
        {
            /// <summary>
            /// 移动或重命名 目录
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <returns>执行器</returns>
            public static IExecutor Execute(in string target, in string source)
            {
                var t = target.Replace('/', Path.DirectorySeparatorChar);
                var s = source.Replace('/', Path.DirectorySeparatorChar);
                return Create().Input($"{CMD_Move} /y \"{t}\" \"{s}\"");
            }
        }
    }
}