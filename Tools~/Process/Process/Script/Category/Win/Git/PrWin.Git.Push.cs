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
            /// 推送
            /// </summary>
            /// <param name="targets">
            /// [Item1 : 文件路径]
            /// [Item2 : 版本分支 默认 master]
            /// </param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Push(ICollection<(string, string)> targets, bool quit = true)
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
                        str.AppendLine(string.Format("@cd /d \"{0}\"", target.Replace('/', '\\')));
                        if (string.IsNullOrEmpty(t.Item2)) str.AppendLine("@git push");
                        else str.AppendLine(string.Format("@git push -u origin \"{0}\"", t.Item2));
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 推送
            /// </summary>
            public static IExecutor Push(ICollection<string> targets, bool quit = true)
            {
                if (targets is null) return new PrException(new ArgumentNullException(nameof(targets))).Execute();
                if (targets.Count == 0) return new PrEmpty().Execute();

                var str = new StringBuilder();
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
                        str.AppendLine("@git push");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 推送
            /// </summary>
            /// <param name="target">
            /// [Item1 : 文件路径]
            /// [Item2 : 版本分支 默认 master]
            /// </param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Push((string, string) target, bool quit = true)
            {
                return Push(new (string, string)[] { target }, quit);
            }

            /// <summary>
            /// 推送
            /// </summary>
            /// <param name="target">文件路径</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Push(string target, bool quit = true)
            {
                return Push(new string[] { target }, quit);
            }
        }
    }
}
