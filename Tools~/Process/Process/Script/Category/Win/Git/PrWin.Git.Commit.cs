/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

namespace AIO
{
    public partial class PrWin
    {
        #region Nested type: Git

        public partial class Git
        {
            #region Nested type: Commit

            /// <summary>
            /// 提交命令
            /// </summary>
            public static class Commit
            {
                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Commit"/>
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
                    if (targets is null) return new PrException(new ArgumentNullException(nameof(targets))).Execute();
                    if (targets.Count == 0) return new PrEmpty().Execute();

                    var str = new StringBuilder();
                    foreach (var (item1, item2) in targets)
                    {
                        var target = item1.Replace('/', '\\');
                        str.AppendLine(LINE_TOP);
                        if (!Directory.Exists(target))
                        {
                            str.AppendFormat("\n @echo Error:{0} \n",
                                             new FileNotFoundException(nameof(target), target).Message).AppendLine();
                        }
                        else
                        {
                            str.AppendLine(string.Format("@echo {0}", Path.GetFileName(target)));
                            str.AppendLine(string.Format("@cd /d \"{0}\"", target));
                            str.AppendLine(string.Format("@set commitArg={0}",
                                                         item2 ?? "default submission information"));
                            str.AppendLine("@git commit -m \"!commitArg!\"");
                        }

                        str.AppendLine(LINE_BOTTOM);
                    }

                    return Git.Execute(str, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Commit"/>
                /// </summary>
                /// <param name="targets">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(ICollection<string> targets, bool quit = true)
                {
                    if (targets is null) return new PrException(new ArgumentNullException(nameof(targets))).Execute();
                    if (targets.Count == 0) return new PrEmpty().Execute();

                    var str = new StringBuilder();
                    str.AppendLine("@set /p commitArg=请输入提交信息 && @echo.");
                    foreach (var target in targets)
                    {
                        str.AppendLine(LINE_TOP);
                        if (!Directory.Exists(target))
                        {
                            str.AppendFormat("\n @echo Error:{0} \n",
                                             new FileNotFoundException(nameof(target), target).Message).AppendLine();
                        }
                        else
                        {
                            str.AppendLine(string.Format("@echo {0}", Path.GetFileName(target)));
                            str.AppendLine(string.Format("@cd /d \"{0}\"", target.Replace('/', '\\')));
                            str.AppendLine("@git commit -m \"!commitArg!\"");
                        }

                        str.AppendLine(LINE_BOTTOM);
                    }

                    return Git.Execute(str, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Commit"/>
                /// </summary>
                /// <param name="target">文件路径</param>
                /// <param name="quit">静默退出</param>
                /// <returns><see cref="IExecutor"/>执行器</returns>
                /// <exception cref="ArgumentNullException">targets is null<code><see cref="ArgumentNullException"/></code></exception>
                public static IExecutor Execute(string target, bool quit = true)
                {
                    return Execute(new[] { target }, quit);
                }

                /// <summary>
                /// 执行 <see cref="PrWin"/> <see cref="Git"/> <see cref="Commit"/>
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
                    return Execute(new[] { target }, quit);
                }
            }

            #endregion
        }

        #endregion
    }
}