/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AIO
{
    public partial class PrWin
    {
        public partial class Git
        {
            /// <summary>
            /// 克隆
            /// </summary>
            public static class Clone
            {
                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Clone"/>
                /// </summary>
                /// <param name="target">目标文件夹</param>
                /// <param name="urls">clone地址列表</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(string target, ICollection<string> urls, bool quit = true)
                {
                    if (urls is null) return new PrException(new ArgumentNullException(nameof(urls))).Execute();
                    if (urls.Count == 0) return new PrEmpty().Execute();
                    if (!Directory.Exists(target)) return new PrException(new Exception($"the destination path does not exist {target}")).Execute();

                    var str = new StringBuilder();
                    foreach (var url in urls)
                    {
                        var name = Path.GetFileName(url).Replace(".git", "").Replace(".ssh", "");
                        var path = Path.Combine(target, name).Replace('/', '\\');
                        str.AppendLine(LINE_TOP);
                        str.AppendLine(string.Format("@echo {0} && @cd /d {1}", path, target));
                        str.AppendLine(string.Format("@git clone {0} && @cd /d {1}", url, path));
                        str.AppendLine("@git submodule update --init --recursive");
                        str.AppendLine(LINE_BOTTOM);
                    }

                    return Git.Execute(str, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Clone"/>
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="url">Clone地址</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(string target, string url, bool quit = true)
                {
                    return Execute(target, new string[] { url }, quit);
                }
            }
        }
    }
}