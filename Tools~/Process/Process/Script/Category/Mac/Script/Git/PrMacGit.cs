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
        /// 
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
                GITPATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Concat(AppDomain.CurrentDomain.BaseDirectory.GetHashCode(), ".sh"));
            }

            #region Function

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
                str.AppendLine("echo $\"|Author      :XINAN                 |\"");
                str.AppendLine("echo $\"|E-MAIL      :1398581458@qq.com     |\"");
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

            #endregion

            #region Remote

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

            #endregion

            #region Clone

            /// <summary>
            /// 克隆
            /// </summary>
            /// <param name="target">目标目录</param>
            /// <param name="urls">git地址</param>
            /// <param name="quit">静默退出</param>
            /// <returns></returns>
            public static IExecutor Clone(string target, ICollection<string> urls, bool quit = true)
            {
                if (urls == null) throw new ArgumentNullException();
                if (urls.Count == 0) return null;
                if (!Directory.Exists(target)) throw new FileNotFoundException($"the destination path does not exist {target}");
                var str = new StringBuilder();
                target = target.Replace('\\', '/');
                str.AppendLine(string.Format("target=\"{0}\" && chmod 777 ${1}", target, "target"));
                foreach (var item in urls)
                {
                    var name = Path.GetFileName(item).Replace(".git", "").Replace(".ssh", "");
                    var path = Path.Combine(target, name).Replace('\\', '/');
                    str.AppendLine(LINE_TOP);
                    str.AppendLine(string.Format("path=\"{0}\"", path));
                    str.AppendLine(string.Format("url=\"{0}\"", item));
                    str.AppendLine("echo $\"${path}\" && cd ${target}");
                    str.AppendLine("git clone ${url}");
                    str.AppendLine("chmod 777 ${path} && cd ${path}");
                    str.AppendLine("git submodule update --init --recursive");


                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 克隆
            /// </summary>
            /// <param name="target">目标文件夹</param>
            /// <param name="urls">clone列表</param>
            /// <param name="quit"></param>
            public static IExecutor Clone(string target, string urls, bool quit = true)
            {
                return Clone(target, new string[] { urls }, quit);
            }

            #endregion

            #region Pull

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

            #endregion

            #region Add

            /// <summary>
            /// 添加修改的文件
            /// </summary>
            /// <param name="targets">文件路径列表</param>
            /// <param name="quit"></param>
            public static IExecutor Add(ICollection<string> targets, bool quit = true)
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
                        str.AppendLine("git add .");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 添加修改的文件
            /// </summary>
            /// <param name="targets">文件路径列表</param>
            /// <param name="quit"></param>
            public static IExecutor Add(string targets, bool quit = true)
            {
                return Add(new string[] { targets }, quit);
            }

            #endregion

            #region Commit

            /// <summary>
            /// 添加修改的文件
            /// </summary>
            /// <param name="targets">
            /// [Item1 : 文件路径]
            /// [Item2 : 提交信息]
            /// </param>
            /// <param name="quit"></param>
            public static IExecutor Commit(ICollection<(string, string)> targets, bool quit = true)
            {
                if (targets == null) throw new ArgumentNullException();
                var str = new StringBuilder();
                foreach (var t in targets)
                {
                    var target = t.Item1.Replace('\\', '/');
                    str.AppendLine(LINE_TOP);
                    if (!Directory.Exists(target))
                    {
                        str.AppendFormat("\n echo $\"Error:{0}$\" \n", new FileNotFoundException(nameof(t), target).Message);
                    }
                    else
                    {
                        str.AppendLine(string.Format("path=\"{0}\"", target.Replace('\\', '/')));
                        str.AppendLine(string.Format("commitArg={0}", t.Item2 ?? "default submission information"));
                        str.AppendLine("echo $\"${path}\" && chmod 777 \"${path}\" && cd \"${path}\"");
                        str.AppendLine("git commit -m \"${commitArg}\"");
                    }

                    str.AppendLine(LINE_BOTTOM);
                }

                return Execute(str, quit);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="targets"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            /// <exception cref="ArgumentNullException"></exception>
            public static IExecutor Commit(ICollection<string> targets, bool quit = true)
            {
                if (targets == null) throw new ArgumentNullException();
                var str = new StringBuilder();
                str.AppendLine("echo $\"请输入提交信息 or please enter the submission information\"");
                str.AppendLine("TIP=\"\"");
                str.AppendLine("read -p \"$TIP\" commitArg");
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
                        str.AppendLine("git commit -m \"${commitArg}\"");
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
            public static IExecutor Commit(string target, bool quit = true)
            {
                return Commit(new string[] { target }, quit);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="target"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            public static IExecutor Commit((string, string) target, bool quit = true)
            {
                return Commit(new (string, string)[] { target }, quit);
            }

            #endregion

            #region Push

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
                        else str.AppendLine("git push");
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
                return Push(new (string, string)[] { target }, quit);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="target"></param>
            /// <param name="quit"></param>
            /// <returns></returns>
            public static IExecutor Push(string target, bool quit = true)
            {
                return Push(new string[] { target }, quit);
            }

            #endregion

            #region Upload

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

            #endregion
        }
    }
}