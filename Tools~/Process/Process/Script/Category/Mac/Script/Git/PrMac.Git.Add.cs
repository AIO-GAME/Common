/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-07-26

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
            /// 添加
            /// </summary>
            public partial class Add
            {
                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Add"/>
                /// </summary>
                /// <param name="targets">文件路径列表</param>
                /// <param name="args">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor Execute(ICollection<string> targets, string args, bool quit = true)
                {
                    var STR = GetExecute(targets, string.Concat("add ", args));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Add"/>
                /// </summary>
                /// <param name="target">文件路径列表</param>
                /// <param name="args">参数</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor Execute(string target, string args, bool quit = true)
                {
                    var STR = GetExecute(target, string.Concat("add ", args));
                    return Git.Execute(STR, quit);
                }

                /// <summary>
                /// 添加修改的文件
                /// </summary>
                /// <param name="targets">文件路径列表</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ALL(ICollection<string> targets, bool quit = true)
                {
                    return Execute(targets, ".", quit);
                }

                /// <summary>
                /// 添加修改的文件
                /// </summary>
                /// <param name="target">文件路径列表</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor ALL(string target, bool quit = true)
                {
                    return Execute(target, ".", quit);
                }
            }
        }
    }
}
