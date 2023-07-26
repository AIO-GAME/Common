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
            /// 
            /// </summary>
            /// <param name="target"></param>
            /// <param name="inputCommit"></param>
            /// <param name="inputOrigin"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            public static IExecutor Upload(string target, bool inputCommit = false, bool inputOrigin = false, bool quit = true)
            {
                return Upload(new string[] { target }, inputCommit, inputOrigin, quit);
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
            /// <param name="quit"></param>
            /// <returns></returns>
            public static IExecutor Upload(ICollection<string> targets, bool inputCommit = false, bool inputOrigin = false, bool quit = true)
            {
                if (targets == null) throw new ArgumentNullException();
                var str = new StringBuilder();

                if (inputCommit)
                {
                    str.AppendLine("echo $\"请输入提交信息 or please enter the submission information\"");
                    str.AppendLine("TIP=\"\"");
                    str.AppendLine("read -p \"$TIP\" commitArg");
                }
                else str.AppendLine("commitArg=default submission information");

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
                        str.AppendLine("git pull -v --progress --allow-unrelated-histories --autostash --stat --recurse-submodules --update --ff-only");
                        str.AppendLine("git add .");
                        str.AppendLine("git commit -m \"${commitArg}\"");
                        if (inputOrigin)
                        {
                            str.AppendLine("echo $\"请推送分支 or please input push origin target\"");
                            str.AppendLine("TIP=\"\"");
                            str.AppendLine("read -p \"$TIP\" originArg");
                            str.AppendLine("git push -u origin \"${originArg}\"");
                        }
                        else str.AppendLine("git push");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }
        }
    }
}