/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;

#endregion

namespace AIO
{
    public partial class PrGit
    {
        #region Nested type: Commit

        /// <summary>
        /// <see cref="PrGit"/> <see cref="Commit"/> 提交
        /// </summary>
        public sealed class Commit
        {
            /// <summary>
            /// 执行
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Execute(in string work, in string args)
            {
                if (string.IsNullOrEmpty(args)) throw new ArgumentNullException(nameof(args));
                return Create(work, "commit {0}", args);
            }

            /// <summary>
            /// Checkout 参数
            /// </summary>
            /// <param name="target">GIT 文件夹</param>
            /// <param name="message">消息</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Default(string target, string message = "-m \"default submission information\"")
            {
                if (string.IsNullOrEmpty(message)) throw new ArgumentNullException(nameof(message));
                return Create(target, "commit \"{0}\"", message);
            }
        }

        #endregion
    }
}