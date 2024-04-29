/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-07-26

|||✩ - - - - - |*/

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

namespace AIO
{
    public partial class PrMac
    {
        #region Nested type: Git

        public partial class Git
        {
            /// <summary>
            /// 推送
            /// </summary>
            /// <param name="targets">
            /// [Item1 : 文件路径]
            /// [Item2 : 版本分支 默认 master]
            /// </param>
            /// <param name="quit"></param>
            public static IExecutor Push(ICollection<(string, string)> targets, bool quit = true)
            {
                if (targets == null) throw new ArgumentNullException();
                var str = new StringBuilder();
                foreach (var t in targets)
                {
                    var target = t.Item1.Replace('\\', '/');
                    str.AppendLine(LINE_TOP);
                    if (!Directory.Exists(target))
                    {
                        str.AppendFormat("\n echo $\"Error:{0}$\" \n", new FileNotFoundException(nameof(target), target).Message);
                    }
                    else
                    {
                        str.AppendLine(string.Format("path=\"{0}\"", target.Replace('\\', '/')));
                        str.AppendLine("echo $\"${path}\" && chmod 777 \"${path}\" && cd \"${path}\"");
                        if (!string.IsNullOrEmpty(t.Item2))
                        {
                            str.AppendLine(string.Format("originArg={0}", t.Item2));
                            str.AppendLine("git push -u origin \"${originArg}\"");
                        }
                        else
                        {
                            str.AppendLine("git push");
                        }
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
                if (targets == null) throw new ArgumentNullException();
                var str = new StringBuilder();
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
                        str.AppendLine("git push");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="target"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            public static IExecutor Push((string, string) target, bool quit = true)
            {
                return Push(new[] { target }, quit);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="target"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            public static IExecutor Push(string target, bool quit = true)
            {
                return Push(new[] { target }, quit);
            }
        }

        #endregion
    }
}