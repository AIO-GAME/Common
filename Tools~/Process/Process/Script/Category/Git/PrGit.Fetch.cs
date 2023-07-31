/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-31
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.IO;

namespace AIO
{
    public partial class PrGit
    {
        /// <summary>
        /// <see cref="PrGit"/> <see cref="Fetch"/>
        /// </summary>
        public static class Fetch
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
                return Create(work, "fetch {0}", args);
            }

            /// <summary>
            /// <see cref="PrGit"/> <see cref="Fetch"/> <see cref="Origin"/>
            /// 同步远程库的数据到本地
            /// 查找 “origin” 是哪一个服务器 从中抓取本地没有的数据，并且更新本地数据库，移动 origin/master 指针指向新的、更新后的位置。
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor Origin(in string work)
            {
                if (string.IsNullOrEmpty(work)) throw new ArgumentNullException(nameof(work));
                if (!Directory.Exists(work)) throw new DirectoryNotFoundException(work);
                return Create(work, "fetch origin");
            }
            
            
            /// <summary>
            /// 同步远程库的数据到本地
            /// </summary>
            /// <param name="work">GIT 文件夹</param>
            /// <returns><see cref="IExecutor"/> – 执行器</returns>
            public static IExecutor ALL(in string work)
            {
                if (string.IsNullOrEmpty(work)) throw new ArgumentNullException(nameof(work));
                if (!Directory.Exists(work)) throw new DirectoryNotFoundException(work);
                return Create(work, "fetch --all");
            }
        }
    }
}