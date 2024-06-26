﻿#region

using System.IO;

#endregion

namespace AIO
{
    public partial class PrCmd
    {
        #region Nested type: Move

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
                return Create().Input(string.Format(
                                          "{0} /y \"{1}\" \"{2}\"",
                                          CMD_Move,
                                          source.Replace('/', Path.PathSeparator),
                                          target.Replace('/', Path.PathSeparator)));
            }
        }

        #endregion
    }
}