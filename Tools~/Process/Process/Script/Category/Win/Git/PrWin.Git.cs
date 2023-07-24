/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
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

            private static IExecutor Execute(StringBuilder context, bool quit = true)
            {
                var co = new StringBuilder(TopInfo());
                co.AppendLine(context.ToString());
                co.AppendLine(quit ? "@exit" : "@pause");

                File.WriteAllText(GITPATH, co.ToString());
                return Open.Path(GITPATH).Link(PrCmd.Del.ALL(GITPATH));
            }
        }
    }
}