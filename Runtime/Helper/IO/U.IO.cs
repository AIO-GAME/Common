using System.Linq;
using SPath = System.IO.Path;
using SDirectory = System.IO.Directory;
using SFile = System.IO.File;

namespace AIO
{
    public static partial class RHelper
    {
        /// <summary>
        /// IO 工具类
        /// </summary>
        public static partial class IO
        {
            /// <summary>
            /// 将指定的资源路径转换为相对于项目“assets”目录的相对路径。
            /// </summary>
            /// <param name="path">资源路径</param>
            /// <returns>相对于“assets”目录的相对路径</returns>
            public static string FromAssets(in string path)
            {
                return AHelper.IO.GetRelativePath(path, Path.Assets);
            }

            /// <summary>
            /// 将指定的项目路径转换为相对于项目根目录的相对路径。
            /// </summary>
            /// <param name="path">项目路径</param>
            /// <returns>相对于项目根目录的相对路径</returns>
            public static string FromProject(in string path)
            {
                return AHelper.IO.GetRelativePath(path, Path.Project);
            }

            /// <summary>
            /// 如果不存在指定文件路径的父目录，则创建它。
            /// </summary>
            /// <param name="path">文件路径</param>
            public static void CreateParentDirectoryIfNeeded(in string path)
            {
                CreateDirectoryIfNeeded(SDirectory.GetParent(path)?.FullName);
            }

            /// <summary>
            /// 如果不存在指定的目录，则创建它。
            /// </summary>
            /// <param name="path">目录路径</param>
            public static void CreateDirectoryIfNeeded(in string path)
            {
                if (!SDirectory.Exists(path)) SDirectory.CreateDirectory(path);
            }

            /// <summary>
            /// 如果存在指定的目录，则删除它及其关联的“.meta”文件。
            /// </summary>
            /// <param name="path">目录路径</param>
            public static void DeleteDirectoryIfExists(in string path)
            {
                if (SDirectory.Exists(path)) SDirectory.Delete(path, true);
                var metaFilePath = SPath.Combine(SPath.GetDirectoryName(path) ?? string.Empty,
                    SPath.GetFileName(path) + ".meta");
                if (SFile.Exists(metaFilePath)) SFile.Delete(metaFilePath);
            }

            /// <summary>
            /// 用指定的替换字符替换文件名中的任何无效字符。该方法使用Path.GetInvalidFileNameChars()方法检索被视为文件名不允许字符的字符数组。
            /// </summary>
            /// <param name="filename">要转换的文件名</param>
            /// <param name="replace">要替换无效字符的字符</param>
            /// <returns>已转换的文件名</returns>
            public static string MakeSafeFilename(string filename, char replace)
            {
                return SPath.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c, replace));
            }
        }
    }
}