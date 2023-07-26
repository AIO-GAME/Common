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
        /// <summary>
        /// GIT BAT 命令
        /// </summary>
        public static partial class Git
        {
            private static readonly string GITPATH;

            static Git()
            {
                GITPATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Concat(AppDomain.CurrentDomain.BaseDirectory.GetHashCode(), ".bat"));
            }

            private const string LINE_TOP = "@echo ─────────────────────────────────────";
            private const string LINE_BOTTOM = "@echo ─────────────────────────────────────&@echo.";

            private static string TopInfo()
            {
                var str = new StringBuilder();
                str.AppendLine("@echo off && @setlocal enabledelayedexpansion");
                str.AppendLine("@chcp 65001 && @color F && @cls && @cd /d %~dp0");
                str.AppendLine("@echo.");
                str.AppendLine("@echo ^┌───────────────────────────────────┐");
                str.AppendLine("@echo ^|Author      :XiNanSky              ^|");
                str.AppendLine("@echo ^|E-MAIL      :1398581458@qq.com     ^|");
                str.AppendLine("@echo ^|Description :Automatic Generation  ^|");
                str.AppendLine("@echo ^└───────────────────────────────────┘");
                str.AppendLine("@echo.");
                return str.ToString();
            }

            /// <summary>
            /// 获取执行器命令
            /// </summary>
            /// <param name="targets">文件路径列表</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="StringBuilder"/></returns>
            private static StringBuilder GetExecute(ICollection<string> targets, string args)
            {
                var str = new StringBuilder();
                if (targets is null || targets.Count == 0) return str;
                foreach (var target in targets)
                {
                    str.AppendLine(LINE_TOP);
                    if (!Directory.Exists(target))
                    {
                        str.AppendFormat("\n @echo Error:{0} \n", new FileNotFoundException(target).Message).AppendLine();
                    }
                    else
                    {
                        str.AppendLine(string.Format("@echo {0}", Path.GetFileName(target)));
                        str.AppendLine(string.Format("cd /d \"{0}\"", target.Replace('/', '\\')));
                        str.AppendLine(string.Format("@git {0}", args));
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return str;
            }

            /// <summary>
            /// 获取执行器命令
            /// </summary>
            /// <param name="target">文件路径列表</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="StringBuilder"/></returns>
            private static StringBuilder GetExecute(string target, string args)
            {
                var str = new StringBuilder();
                if (string.IsNullOrEmpty(target)) return str;

                str.AppendLine(LINE_TOP);
                if (!Directory.Exists(target))
                {
                    str.AppendFormat("\n @echo Error:{0} \n", new FileNotFoundException(target).Message).AppendLine();
                }
                else
                {
                    str.AppendLine(string.Format("@echo {0}", Path.GetFileName(target)));
                    str.AppendLine(string.Format("cd /d \"{0}\"", target.Replace('/', '\\')));
                    str.AppendLine(string.Format("@git {0}", args));
                }

                str.AppendLine(LINE_BOTTOM);

                return str;
            }

            private static IExecutor Execute(StringBuilder context, bool quit = true)
            {
                if (context is null) return new PrException(new ArgumentNullException(nameof(context))).Execute();
                if (context.Length == 0) return new PrEmpty().Execute();

                var co = new StringBuilder(TopInfo());
                co.AppendLine(context.ToString());
                co.AppendLine(quit ? "@exit" : "@pause");

                File.WriteAllText(GITPATH, co.ToString());
                return Open.Path(GITPATH).Link(PrCmd.Del.ALL(GITPATH));
            }
        }
    }
}