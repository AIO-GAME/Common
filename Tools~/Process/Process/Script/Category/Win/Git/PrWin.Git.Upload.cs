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

        public static partial class Git
        {
            /// <summary>
            /// 1:拉取远端库
            /// 2:添加修改文件
            /// 3:提交本地修改信息
            /// 4:上传本地库到远端库
            /// </summary>
            /// <param name="target">目标库</param>
            /// <param name="inputOrigin">上传分支</param>
            /// <param name="inputCommit">提交信息</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Upload(string target, bool inputCommit = false, bool inputOrigin = false, bool quit = true)
            {
                return Upload(new[] { target }, inputCommit, inputOrigin, quit);
            }

            /// <summary>
            /// 1:拉取远端库
            /// 2:添加修改文件
            /// 3:提交本地修改信息
            /// 4:上传本地库到远端库
            /// </summary>
            /// <param name="targets">目标库</param>
            /// <param name="inputOrigin">上传分支</param>
            /// <param name="inputCommit">提交信息</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Upload(ICollection<string> targets, bool inputCommit = false, bool inputOrigin = false, bool quit = true)
            {
                if (targets is null) return new PrException(new ArgumentNullException(nameof(targets))).Execute();
                if (targets.Count == 0) return new PrEmpty().Execute();

                var str = new StringBuilder();
                if (inputCommit == false) str.AppendLine("@set commitArg=default submission information");
                else str.AppendLine("@echo please input push commit info && @set /p commitArg=");

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
                        str.AppendLine("@git pull -v --progress --allow-unrelated-histories --autostash --stat --recurse-submodules --update --ff-only");
                        str.AppendLine("@git add .");
                        str.AppendLine("@git commit -m \"!commitArg!\"");
                        if (inputOrigin)
                        {
                            str.AppendLine("@echo please input push origin target && @set /p originArg=");
                            str.AppendLine("@git push -u origin \"!originArg!\"");
                        }
                        else
                        {
                            str.AppendLine("@git push");
                        }
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }
        }

        #endregion
    }
}