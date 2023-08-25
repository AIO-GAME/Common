/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
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
            /// 提交命令
            /// </summary>
            public static class Commit
            {
                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Commit"/>
                /// </summary>
                /// <param name="targets">
                /// [Item1 : 文件路径]
                /// [Item2 : 提交信息]
                /// </param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(ICollection<(string, string)> targets, bool quit = true)
                {
                    if (targets == null) throw new ArgumentNullException();
                    var str = new StringBuilder();
                    foreach (var t in targets)
                    {
                        var target = t.Item1.Replace('\\', '/');
                        str.AppendLine(LINE_TOP);
                        if (!Directory.Exists(target))
                        {
                            str.AppendFormat("\n echo $\"Error:{0}$\" \n", new FileNotFoundException(nameof(t), target).Message);
                        }
                        else
                        {
                            str.AppendLine(string.Format("path=\"{0}\"", target.Replace('\\', '/')));
                            str.AppendLine(string.Format("commitArg={0}", t.Item2 ?? "default submission information"));
                            str.AppendLine("echo $\"${path}\" && chmod 777 \"${path}\" && cd \"${path}\"");
                            str.AppendLine("git commit -m \"${commitArg}\"");
                        }

                        str.AppendLine(LINE_BOTTOM);
                    }

                    return Git.Execute(str, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Commit"/>
                /// </summary>
                /// <param name="targets">
                /// [Item1 : 文件路径]
                /// [Item2 : 提交信息]
                /// </param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(ICollection<string> targets, bool quit = true)
                {
                    if (targets == null) throw new ArgumentNullException();
                    var str = new StringBuilder();
                    str.AppendLine("echo $\"请输入提交信息 or please enter the submission information\"");
                    str.AppendLine("TIP=\"\"");
                    str.AppendLine("read -p \"$TIP\" commitArg");
                    foreach (var target in targets)
                    {
                        str.AppendLine(LINE_TOP);
                        if (!Directory.Exists(target))
                        {
                            str.AppendFormat("\n echo $\"Error:{0}$\" \n", new FileNotFoundException(nameof(target), target).Message);
                        }
                        else
                        {
                            str.AppendLine(string.Format("path=\"{0}\"", target.Replace('\\', '/')));
                            str.AppendLine("echo $\"${path}\" && chmod 777 \"${path}\" && cd \"${path}\"");
                            str.AppendLine("git commit -m \"${commitArg}\"");
                        }

                        str.AppendLine(LINE_BOTTOM);
                    }

                    return Git.Execute(str, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Commit"/>
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(string target, bool quit = true)
                {
                    return Execute(new string[] { target }, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrMac"/> <see cref="Git"/> <see cref="Commit"/>
                /// </summary>
                /// <param name="target">
                /// [Item1 : 文件路径]
                /// [Item2 : 提交信息]
                /// </param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute((string, string) target, bool quit = true)
                {
                    return Execute(new (string, string)[] { target }, quit);
                }
            }
        }
    }
}
