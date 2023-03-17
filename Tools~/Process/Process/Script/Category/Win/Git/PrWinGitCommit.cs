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
        public static partial class Git
        {
            /// <summary>
            /// 提交
            /// </summary>
            /// <param name="targets">
            /// [Item1 : 文件路径]
            /// [Item2 : 提交信息]
            /// </param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Commit(ICollection<(string, string)> targets, bool quit = true)
            {
                if (targets is null) return new PrException(new ArgumentNullException(nameof(targets))).Execute();
                if (targets.Count == 0) return new PrEmpty().Execute();

                var str = new StringBuilder();
                foreach (var t in targets)
                {
                    var target = t.Item1.Replace('/', '\\');
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
                            t.Item2 ?? "default submission information"));
                        str.AppendLine("@git commit -m \"!commitArg!\"");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 提交
            /// </summary>
            /// <param name="targets">文件路径</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Commit(ICollection<string> targets, bool quit = true)
            {
                if (targets is null) return new PrException(new ArgumentNullException(nameof(targets))).Execute();
                if (targets.Count == 0) return new PrEmpty().Execute();

                var str = new StringBuilder();
                str.AppendLine($"@set /p commitArg=请输入提交信息 && @echo.");
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

                return Execute(str, quit);
            }

            /// <summary>
            /// 提交
            /// </summary>
            /// <param name="target">文件路径</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Commit(string target, bool quit = true)
            {
                return Commit(new string[] { target }, quit);
            }

            /// <summary>
            /// 提交
            /// </summary>
            /// <param name="target">文件路径</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Commit((string, string) target, bool quit = true)
            {
                return Commit(new (string, string)[] { target }, quit);
            }
        }
    }
}