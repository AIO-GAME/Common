#region

using System.IO;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 判断当前路径是否存在 不管是以文件夹 还是 文件的形式存在 不会自动对路径转义 请自行转义
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Exists(string path)
            {
                return ExistsFile(path) || ExistsDir(path);
            }

            /// <summary>
            /// 判断当前路径是否存在 文件夹和文件都存在才返回True 不会自动对路径转义 请自行转义
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsBoth(string path)
            {
                return ExistsFile(path) && ExistsDir(path);
            }

            /// <summary>
            /// 判断文件是否存在 不会自动对路径转义 请自行转义
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsFile(in string path)
            {
                return File.Exists(path);
            }

            /// <summary>
            /// 判断目录是否存在 不会自动对路径转义 请自行转义
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsDir(in string path)
            {
                return Directory.Exists(path);
            }

            /// <summary>
            /// 判断目录是否存在 会自动对路径转义 标准正斜杠
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsDirEx(in string path)
            {
                return Directory.Exists(path.Replace("\\", "/"));
            }

            /// <summary>
            /// 判断文件是否存在 会自动对路径转义 标准正斜杠
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsFileEx(in string path)
            {
                return File.Exists(path.Replace("\\", "/"));
            }

            /// <summary>
            /// 判断当前路径是否存在 不管是以文件夹 还是 文件的形式存在 会自动对路径转义 标准正斜杠
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsEx(string path)
            {
                return ExistsFileEx(path) || ExistsDirEx(path);
            }

            /// <summary>
            /// 判断当前路径是否存在 文件夹和文件都存在才返回True 会自动对路径转义 标准正斜杠
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsBothEx(string path)
            {
                return ExistsFileEx(path) && ExistsDirEx(path);
            }
        }

        #endregion
    }
}