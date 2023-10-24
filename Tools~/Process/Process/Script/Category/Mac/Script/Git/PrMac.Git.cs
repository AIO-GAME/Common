/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-11-23                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AIO
{
    public sealed partial class PrMac
    {
        /// <summary>
        /// Git 命令
        /// </summary>
        public static partial class Git
        {
            private static readonly string GITPATH;

            static Git()
            {
                var result = Which(CMD_Git).Sync();
                if (!File.Exists(result.StdOut.ToString()))
                {
                    Console.WriteLine("{0} 未安装 程序自动安装中 Missing : {1}", CMD_Git, result.StdOut);
                    Brew.Install(CMD_Git).Sync();
                }

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.GetName().Name.StartsWith("UnityEngine")) continue;
                    var type = assembly.GetType("UnityEngine.Application");
                    if (type is null) continue;
                    var dataPath = type.GetProperty("dataPath",
                        System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                    if (dataPath is null) continue;
                    GITPATH = dataPath.GetValue(null, null) as string;
                    if (string.IsNullOrEmpty(GITPATH)) continue;
                    var Root = Directory.GetParent(GITPATH);
                    if (Root is null)
                    {
                        GITPATH = string.Empty;
                        continue;
                    }

                    GITPATH = Path.Combine(Root.FullName,
                        string.Concat(AppDomain.CurrentDomain.BaseDirectory.GetHashCode(), ".sh"));
                    break;
                }

                if (string.IsNullOrEmpty(GITPATH))
                {
                    GITPATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                        string.Concat(AppDomain.CurrentDomain.BaseDirectory.GetHashCode(), ".sh"));
                }
            }

            /// <summary>
            /// 获取执行器命令
            /// </summary>
            /// <param name="targets">文件路径列表</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="StringBuilder"/></returns>
            private static StringBuilder GetExecute(ICollection<string> targets, string args)
            {
                if (targets == null) throw new ArgumentNullException(nameof(targets));
                var str = new StringBuilder();
                if (targets.Count == 0) return str;
                foreach (var target in targets)
                {
                    if (string.IsNullOrEmpty(target)) throw new ArgumentNullException(nameof(target));
                    str.AppendLine(LINE_TOP);
                    if (!Directory.Exists(target))
                    {
                        str.AppendFormat("\n echo $\"Error:{0}$\" \n",
                            new FileNotFoundException(nameof(target), target).Message);
                    }
                    else
                    {
                        str.AppendLine(string.Format("path=\"{0}\"", target.Replace('\\', '/')));
                        str.AppendLine("echo $\"${path}\" && chmod 777 ${path} && cd ${path}");
                        str.AppendLine($"git {args}");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return str;
            }

            /// <summary>
            /// 拉取
            /// </summary>
            /// <param name="target">文件路径列表</param>
            /// <param name="args">参数</param>
            /// <returns><see cref="StringBuilder"/></returns>
            private static StringBuilder GetExecute(string target, string args)
            {
                if (string.IsNullOrEmpty(target)) throw new ArgumentNullException(nameof(target));
                var str = new StringBuilder();
                str.AppendLine(LINE_TOP);
                if (!Directory.Exists(target))
                {
                    str.AppendFormat("\n echo $\"Error:{0}$\" \n",
                        new FileNotFoundException(nameof(target), target).Message);
                }
                else
                {
                    str.AppendLine(string.Format("path=\"{0}\"", target.Replace('\\', '/')));
                    str.AppendLine("echo $\"${path}\" && chmod 777 ${path} && cd ${path}");
                    str.AppendLine($"git {args}");
                }

                str.AppendLine(LINE_BOTTOM);
                return str;
            }


            private const string LINE_TOP = "echo $\"─────────────────────────────────────\"";
            private const string LINE_BOTTOM = "echo $\"─────────────────────────────────────\" && echo";

            /// <summary>
            /// 头部信息
            /// </summary>
            private static string TopInfo()
            {
                var str = new StringBuilder();
                str.AppendLine("#!/bin/zsh");
                str.AppendLine("PATH=/bin:/sbin:/usr/bin:/usr/sbin:/usr/local/bin:/usr/local/sbin:~/bin");
                str.AppendLine("export PATH");
                str.AppendLine("echo $\"┌───────────────────────────────────┐\"");
                str.AppendLine("echo $\"|Description :Automatic Generation  |\"");
                str.AppendLine("echo $\"└───────────────────────────────────┘\"");
                str.AppendLine("echo");
                str.AppendLine("current=$(cd $(dirname $0); pwd)");
                return str.ToString();
            }

            private static string Pause()
            {
                var str = new StringBuilder();
                str.AppendLine("function get_char()");
                str.AppendLine("{");
                str.AppendLine("  SAVEDSTTY=`stty -g` && stty -echo && stty cbreak");
                str.AppendLine("  dd if=/dev/tty bs=1 count=1 2> /dev/null");
                str.AppendLine("  stty -raw && stty echo && stty $SAVEDSTTY");
                str.AppendLine("}");
                str.AppendLine("enable_pause=1");
                str.AppendLine("function pause()");
                str.AppendLine("{");
                str.AppendLine("  if [ \"x$1\" != \"x\" ]; then");
                str.AppendLine("    echo $1");
                str.AppendLine("  fi");
                str.AppendLine("  if [ $enable_pause -eq 1 ]; then");
                str.AppendLine("    echo \"Press any key to continue!\"");
                str.AppendLine("    char=`get_char`");
                str.AppendLine("  fi");
                str.AppendLine("}");
                return str.ToString();
            }

            /// <summary>
            /// 执行文件
            /// </summary>
            private static IExecutor Execute(StringBuilder context, bool quit = false)
            {
                var co = new StringBuilder(TopInfo());
                co.AppendLine(context.ToString());
                if (quit) co.AppendLine("exit");
                else co.AppendLine(Pause()).AppendLine("pause \"\"");

                File.WriteAllText(GITPATH, co.ToString(), Encoding.UTF8);
                return Open.Shell(GITPATH);
            }
        }
    }
}