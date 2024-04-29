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
            /// 设置远端镜像库
            /// </summary>
            /// <param name="target">Git仓库</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor RemoteSetUrl(string target, bool quit = true)
            {
                return RemoteSetUrl(new[] { target }, quit);
            }

            /// <summary>
            /// 设置远端镜像库
            /// </summary>
            /// <param name="targets">Git仓库</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor RemoteSetUrl(ICollection<string> targets, bool quit = true)
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
                        str.AppendLine(string.Format("@echo {0} && @cd /d {1}", Path.GetFileName(target), target));
                        str.AppendLine("@set /p remoteName=请输入关联的远端库名称 && @echo.");
                        str.AppendLine("@set /p url=请输入关联的远端库地址 && @echo.");
                        str.AppendLine("@git remote set-url --add origin \"%url%\"");
                        str.AppendLine("@git remote add \"%remoteName%\" \"%url%\"");
                        str.AppendLine("@git lfs push origin --all");
                        str.AppendLine("@git pull \"%remoteName%\" master -f -t --allow-unrelated-histories");
                        str.AppendLine("@git merge \"%remoteName%\" --allow-unrelated-histories");
                        str.AppendLine("@git add .");
                        str.AppendLine("@git commit -m \"remote set-url add origin %url%\"");
                        str.AppendLine("@git push -u origin master");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }
        }

        #endregion
    }
}