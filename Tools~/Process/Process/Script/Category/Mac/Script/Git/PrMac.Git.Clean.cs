/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-07-26
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AIO
{
    public partial class PrMac
    {
        public partial class Git
        {
            /// <summary>
            /// 清理
            /// </summary>
            public static class Clean
            {
                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Clean"/>
                /// </summary>
                /// <param name="targets">文件路径列表</param>
                /// <param name="agrs">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor Execute(ICollection<string> targets, string agrs, bool quit = true)
                {
                    var STR = GetExecute(targets, string.Concat("clean ", agrs));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 强制清理文件夹 并不使用用.gitignore 中忽略的文件
                /// </summary>
                /// <param name="targets">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ForceDirX(ICollection<string> targets, bool quit = true)
                {
                    return Execute(targets, "-fd -x", quit);
                }

                /// <summary>
                /// 强制清理文件夹
                /// </summary>
                /// <param name="targets">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ForceDir(ICollection<string> targets, bool quit = true)
                {
                    return Execute(targets, "-fd", quit);
                }


                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Clean"/>
                /// </summary>
                /// <param name="target">文件路径列表</param>
                /// <param name="agrs">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor Execute(string target, string agrs, bool quit = true)
                {
                    var STR = GetExecute(target, string.Concat("clean ", agrs));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 强制清理文件夹 并不使用用.gitignore 中忽略的文件
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ForceDirX(string target, bool quit = true)
                {
                    return Execute(target, "-fd -x", quit);
                }

                /// <summary>
                /// 强制清理文件夹
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ForceDir(string target, bool quit = true)
                {
                    return Execute(target, "-fd", quit);
                }
            }
        }
    }
}