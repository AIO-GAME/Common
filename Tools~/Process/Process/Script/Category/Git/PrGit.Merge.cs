/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-07-31

|||✩ - - - - - |*/

using System;
using System.IO;

namespace AIO
{
    public partial class PrGit
    {
        /// <summary>
        /// <see cref="PrGit"/> <see cref="Merge"/>
        /// </summary>
        public static class Merge
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
                if (!Directory.Exists(work)) throw new DirectoryNotFoundException(work);
                return Create(work, "commit {0}", args);
            }
        }
    }
}
