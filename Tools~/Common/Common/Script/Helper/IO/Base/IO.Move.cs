#region

using System.IO;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        /// <summary>
        /// IO工具类
        /// </summary>
        public partial class IO
        {
            /// <summary>
            /// 移动文件
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="overlay">Ture:覆盖 False:不覆盖</param>
            public static void MoveFile(
                string source,
                string target,
                bool   overlay = false
            )
            {
                source = source.Replace('\\', Path.AltDirectorySeparatorChar);
                target = target.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!File.Exists(source)) return;

                if (File.Exists(target))
                {
                    if (overlay) File.Delete(target);
                    else return;
                }
                else
                {
                    var dir = Path.GetDirectoryName(target);
                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
                }

                File.Move(source, target);
            }

            /// <summary>
            /// 移动文件夹
            /// </summary>
            /// <param name="source">源路径</param>
            /// <param name="target">目标路径</param>
            /// <param name="overlay">Ture:覆盖 False:不覆盖</param>
            public static void MoveDir(
                string source,
                string target,
                bool   overlay = false
            )
            {
                source = source.Replace('\\', Path.AltDirectorySeparatorChar);
                target = target.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!Directory.Exists(source)) return;
                if (Directory.Exists(target))
                {
                    if (overlay) Directory.Delete(target);
                    else return;
                }

                Directory.Move(source, target);
            }
        }

        #endregion
    }
}