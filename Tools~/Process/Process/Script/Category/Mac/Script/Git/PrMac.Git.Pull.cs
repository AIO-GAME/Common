/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
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
            /// 拉取
            /// </summary>
            /// <param name="targets">文件路径列表</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Pull(ICollection<string> targets, bool quit = true)
            {
                if (targets == null) throw new ArgumentNullException(nameof(targets));
                if (targets.Count == 0) return null;
                var str = new StringBuilder();
                foreach (var target in targets)
                {
                    if (string.IsNullOrEmpty(target)) throw new ArgumentNullException(nameof(target));
                    str.AppendLine(LINE_TOP);
                    if (!Directory.Exists(target))
                    {
                        str.AppendFormat("\n echo $\"Error:{0}$\" \n", new FileNotFoundException(nameof(target), target).Message);
                    }
                    else
                    {
                        str.AppendLine(string.Format("path=\"{0}\"", target.Replace('\\', '/')));
                        str.AppendLine("echo $\"${path}\" && chmod 777 ${path} && cd ${path}");
                        str.AppendLine("git pull -v --progress --allow-unrelated-histories --autostash --stat --recurse-submodules --update --ff-only");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 拉取
            /// </summary>
            /// <param name="target">文件路径</param>
            /// <param name="quit">静默退出</param>
            public static IExecutor Pull(string target, bool quit = true)
            {
                return Pull(new string[] { target }, quit);
            }
        }
    }
}
