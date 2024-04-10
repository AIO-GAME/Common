#region

using System;
using System.IO;

#endregion

namespace AIO
{
    public sealed partial class PrMac
    {
        #region Nested type: Duti

        //     -s    : 选项会让 duti 从命令行读参数.
        //     -x ext: 选项会让 duti 输出参数中扩展名对应的默认应用
        //     -d uti: 打印 UTI 的默认应用.
        //     -l uti: 打印 UTI 的所有可用应用.
        //     -V    : 打印版本.
        //     -v    : 详细输出.
        //     -h    : 帮助.

        /// <summary>
        /// Mac 设置默认打开程序
        /// </summary>
        public sealed class Duti
        {
            static Duti()
            {
                var result = Which(CMD_Duti).Sync();
                if (!File.Exists(result.StdOut.ToString()))
                {
                    Console.WriteLine("{0} 未安装 程序自动安装中 Missing : {1}", CMD_Duti, result.StdOut);
                    Brew.Install(CMD_Duti).Sync();
                }
            }

            //all:      application handles all roles for the given UTI.
            //viewer:   application handles reading and displaying documents with the given UTI.
            //editor:   application can manipulate and save the item.Implies viewer.
            //shell:    application can execute the item.
            //none:     application cannot open the item, but provides an icon for the given UTI.

            /// <summary>
            /// 修改指定类型文件默认打开方式
            /// </summary>
            /// <param name="appName">Bundle ID</param>
            /// <param name="uti">UTI类型</param>
            /// <param name="role">文件角色:all,viewer,editor,shell,none</param>
            public static IExecutor DefaultPrograms(string appName, string uti, string role = "all")
            {
                if (string.IsNullOrEmpty(appName)) throw new ArgumentException($"“{nameof(appName)}”不能为 null 或空。", nameof(appName));
                if (string.IsNullOrEmpty(uti)) throw new ArgumentException($"“{nameof(uti)}”不能为 null 或空。", nameof(uti));
                if (string.IsNullOrEmpty(role)) throw new ArgumentException($"“{nameof(role)}”不能为 null 或空。", nameof(role));
                return Create(CMD_Duti, "-s {0} {1} {2}", appName, uti, role);
            }
        }

        #endregion
    }
}