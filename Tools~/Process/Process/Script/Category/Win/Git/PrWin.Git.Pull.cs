﻿/*|============================================|*|
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

        public static partial class Git
        {
            /// <summary>
            /// 拉取
            /// </summary>
            /// <param name="targets">文件路径列表</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Pull(ICollection<string> targets, bool quit = true)
            {
                if (targets is null) return new PrException(new ArgumentNullException(nameof(targets))).Execute();
                if (targets.Count == 0) return new PrEmpty().Execute();
                var str = new StringBuilder();
                foreach (var target in targets)
                {
                    str.AppendLine(LINE_TOP);
                    if (!Directory.Exists(target))
                    {
                        str.AppendFormat("\n @echo Error:{0} \n", new FileNotFoundException(nameof(target), target).Message).AppendLine();
                    }
                    else
                    {
                        str.AppendLine(string.Format("@echo {0}", Path.GetFileName(target)));
                        str.AppendLine(string.Format("@cd /d {0}", target.Replace('/', '\\')));
                        str.AppendLine("@git pull --all -v --progress --allow-unrelated-histories --autostash --stat --recurse-submodules --update --ff-only --rebase");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 拉取
            /// </summary>
            /// <param name="targets">文件路径列表</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor PullBranch(ICollection<string> targets, bool quit = true)
            {
                if (targets is null) return new PrException(new ArgumentNullException(nameof(targets))).Execute();
                if (targets.Count == 0) return new PrEmpty().Execute();
                var str = new StringBuilder();
                str.AppendLine("@echo 请输入远端名称");
                str.AppendLine("@set /p origin=");
                str.AppendLine("@echo 请输入远端分支名称");
                str.AppendLine("@set /p barnch=");
                foreach (var target in targets)
                {
                    str.AppendLine(LINE_TOP);
                    if (!Directory.Exists(target))
                    {
                        str.AppendFormat("\n @echo Error:{0} \n", new FileNotFoundException(nameof(target), target).Message).AppendLine();
                    }
                    else
                    {
                        str.AppendLine(string.Format("@echo {0}", Path.GetFileName(target)));
                        str.AppendLine(string.Format("@cd /d {0}", target.Replace('/', '\\')));
                        str.AppendLine("@git pull %origin% %barnch% -v --progress --allow-unrelated-histories --autostash --stat --recurse-submodules --update --ff-only --rebase");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 拉取
            /// </summary>
            /// <param name="target">文件路径列表</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Pull(in string target, in bool quit = true)
            {
                return Pull(new[] { target }, quit);
            }
        }

        #endregion
    }
}