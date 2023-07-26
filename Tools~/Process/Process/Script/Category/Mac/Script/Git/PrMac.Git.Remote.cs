/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
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
            /// 设置远端镜像库
            /// </summary>
            /// <param name="targets">Git仓库</param>
            /// <param name="quit">自动退出</param>
            public static IExecutor RemoteSetUrl(string targets, bool quit = true)
            {
                return RemoteSetUrl(new string[] { targets }, quit);
            }

            /// <summary>
            /// 设置远端镜像库
            /// </summary>
            /// <param name="targets">Git仓库</param>
            /// <param name="quit">自动退出</param>
            public static IExecutor RemoteSetUrl(ICollection<string> targets, bool quit = true)
            {
                if (targets is null) throw new ArgumentNullException(nameof(targets));
                if (targets.Count == 0) return null;
                var str = new StringBuilder();
                str.AppendLine("echo $\"请输入关联的远端库名称 or Please enter the name of the associated remote library\"");
                str.AppendLine("TIP=\"\"");
                str.AppendLine("read -p \"$TIP\" remoteName");
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

                        str.AppendLine("echo $\"请输入关联的远端库地址 or Please enter the associated remote library address\"");
                        str.AppendLine("TIP=\"\"");
                        str.AppendLine("read -p \"$TIP\" url");

                        str.AppendLine("git remote set-url --add origin \"${url}\"");
                        str.AppendLine("git remote add \"${remoteName}\" \"${url}\"");
                        str.AppendLine("git lfs push origin --all");
                        str.AppendLine("git pull \"${remoteName}\" master -f -t --allow-unrelated-histories");
                        str.AppendLine("git merge \"${remoteName}\" --allow-unrelated-histories");
                        str.AppendLine("git add .");
                        str.AppendLine("git commit -m \"remote set-url add origin ${url}\"");
                        str.AppendLine("git push -u origin master");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }
        }
    }
}