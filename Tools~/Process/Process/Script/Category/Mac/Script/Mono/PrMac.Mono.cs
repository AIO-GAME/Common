#region

using System;
using System.IO;

#endregion

namespace AIO
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class PrMac
    {
        #region Nested type: Mono

        /// <summary>
        /// Mono
        /// </summary>
        public static class Mono
        {
            static Mono()
            {
                var result = Which(CMD_Mono).Sync();
                if (!File.Exists(result.StdOut.ToString()))
                {
                    Console.WriteLine("mono 未安装 程序自动安装中");
                    Brew.Install(CMD_Mono).Sync();
                }
                else
                {
                    Console.WriteLine("mono 已安装 不需要重新安装");
                }
            }

            //all:      application handles all roles for the given UTI.
            //viewer:   application handles reading and displaying documents with the given UTI.
            //editor:   application can manipulate and save the item.Implies viewer.
            //shell:    application can execute the item.
            //none:     application cannot open the item, but provides an icon for the given UTI.

            /// <summary>
            ///
            /// </summary>
            /// <param name="exe"></param>
            /// <param name="command"></param>
            public static IExecutor Exe(string exe, params string[] command)
            {
                return Create(CMD_Mono, $"{exe} {string.Join(" ", command)}");
            }
        }

        #endregion
    }
}