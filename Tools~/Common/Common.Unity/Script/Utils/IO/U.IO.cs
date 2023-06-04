using System.IO;
using System.Linq;

using AUtils = Utils;

public static partial class UtilsEngine
{
    public static partial class IO
    {
        /// <summary>
        /// 将指定的资源路径转换为相对于项目“assets”目录的相对路径。
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns>相对于“assets”目录的相对路径</returns>
        public static string FromAssets(in string path)
        {
            return AUtils.IO.GetRelativePath(path, UtilsEngine.Path.Assets);
        }

        /// <summary>
        /// 将指定的项目路径转换为相对于项目根目录的相对路径。
        /// </summary>
        /// <param name="path">项目路径</param>
        /// <returns>相对于项目根目录的相对路径</returns>
        public static string FromProject(in string path)
        {
            return AUtils.IO.GetRelativePath(path, UtilsEngine.Path.Project);
        }

        /// <summary>
        /// 如果不存在指定文件路径的父目录，则创建它。
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void CreateParentDirectoryIfNeeded(in string path)
        {
            CreateDirectoryIfNeeded(Directory.GetParent(path)?.FullName);
        }

        /// <summary>
        /// 如果不存在指定的目录，则创建它。
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void CreateDirectoryIfNeeded(in string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 如果存在指定的目录，则删除它及其关联的“.meta”文件。
        /// </summary>
        /// <param name="path">目录路径</param>
        public static void DeleteDirectoryIfExists(in string path)
        {
            if (Directory.Exists(path)) Directory.Delete(path, true);
            var metaFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path) ?? string.Empty, System.IO.Path.GetFileName(path) + ".meta");
            if (File.Exists(metaFilePath)) File.Delete(metaFilePath);
        }

        /// <summary>
        /// 用指定的替换字符替换文件名中的任何无效字符。该方法使用Path.GetInvalidFileNameChars()方法检索被视为文件名不允许字符的字符数组。
        /// </summary>
        /// <param name="filename">要转换的文件名</param>
        /// <param name="replace">要替换无效字符的字符</param>
        /// <returns>已转换的文件名</returns>
        public static string MakeSafeFilename(string filename, char replace)
        {
            return System.IO.Path.GetInvalidFileNameChars().Aggregate(filename, (current, c) => current.Replace(c, replace));
        }
    }
}
