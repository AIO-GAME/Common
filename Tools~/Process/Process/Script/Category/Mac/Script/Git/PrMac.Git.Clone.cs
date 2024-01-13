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
            /// 克隆
            /// </summary>
            public static class Clone
            {
                #region Clone

                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Clone"/>
                /// </summary>
                /// <param name="target">目标目录</param>
                /// <param name="urls">git地址</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor Execute(string target, ICollection<string> urls, bool quit = true)
                {
                    if (urls == null) throw new ArgumentNullException();
                    if (urls.Count == 0) return null;
                    if (!Directory.Exists(target)) throw new FileNotFoundException($"the destination path does not exist {target}");
                    var str = new StringBuilder();
                    target = target.Replace('\\', '/');
                    str.AppendLine(string.Format("target=\"{0}\" && chmod 777 ${1}", target, "target"));
                    foreach (var item in urls)
                    {
                        var name = Path.GetFileName(item).Replace(".git", "").Replace(".ssh", "");
                        var path = Path.Combine(target, name).Replace('\\', '/');
                        str.AppendLine(LINE_TOP);
                        str.AppendLine(string.Format("path=\"{0}\"", path));
                        str.AppendLine(string.Format("url=\"{0}\"", item));
                        str.AppendLine("echo $\"${path}\" && cd ${target}");
                        str.AppendLine("git clone ${url}");
                        str.AppendLine("chmod 777 ${path} && cd ${path}");
                        str.AppendLine("git submodule update --init --recursive");


                        str.AppendLine(LINE_BOTTOM);
                    }

                    return Git.Execute(str, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Clone"/>
                /// </summary>
                /// <param name="target">目标目录</param>
                /// <param name="urls">git地址</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                public static IExecutor Execute(string target, string urls, bool quit = true)
                {
                    return Execute(target, new string[] { urls }, quit);
                }

                #endregion
            }
        }
    }
}
