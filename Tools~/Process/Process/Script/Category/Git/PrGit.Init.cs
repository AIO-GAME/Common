/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;

namespace AIO
{
    public partial class PrGit
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public sealed class Init
        {
            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="args">参数</param>
            /// <returns>执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new ArgumentNullException(nameof(args));
                return Create(work, "init {0}", args);
            }
        }
    }
}