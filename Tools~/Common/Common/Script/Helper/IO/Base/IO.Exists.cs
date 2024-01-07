/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

using System.IO;
using System.Runtime.CompilerServices;

namespace AIO
{
    public partial class AHelper
    {
        public partial class IO
        {
            /// <summary>
            /// 判断当前路径是否存在 不管是以文件夹 还是 文件的形式存在
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool Exists(string path)
            {
                return ExistsFile(path) || ExistsFolder(path);
            }
            
            /// <summary>
            /// 判断当前路径是否存在 文件夹和文件都存在才返回True
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsBoth(string path)
            {
                return ExistsFile(path) && ExistsFolder(path);
            }

            /// <summary>
            /// 判断文件是否存在
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsFile(in string path)
            {
                return File.Exists(path);
            }

            /// <summary>
            /// 判断目录是否存在
            /// </summary>
            /// <returns>True:存在 False:不存在</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool ExistsFolder(in string path)
            {
                return Directory.Exists(path);
            }
        }
    }
}