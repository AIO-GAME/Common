#region

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 删除文件夹
            /// </summary>
            public static void DeleteDir(
                in DirectoryInfo directory,
                in SearchOption  option = SearchOption.AllDirectories,
                in bool          isAll  = false)
            {
                if (!directory.Exists) return;
                if (isAll) Parallel.ForEach(directory.GetFiles("*", option), file => { DeleteFile(file); });
                directory.Delete(isAll);
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            public static void DeleteDir(
                in string       director,
                in SearchOption option = SearchOption.AllDirectories,
                in bool         isAll  = false)
            {
                DeleteDir(new DirectoryInfo(director), option, isAll);
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            public static void DeleteDir(
                in IEnumerable<string> directors,
                SearchOption           option = SearchOption.AllDirectories,
                bool                   isAll  = false)
            {
                Parallel.ForEach(directors, folder => { DeleteDir(new DirectoryInfo(folder), option, isAll); });
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            public static void DeleteDir(
                in IEnumerable<DirectoryInfo> directors,
                SearchOption                  option = SearchOption.AllDirectories,
                bool                          isAll  = false)
            {
                Parallel.ForEach(directors, folder => { DeleteDir(folder, option, isAll); });
            }

            #region Delete File

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="path">文件相对路径</param>
            public static void DeleteFile(in string path)
            {
                if (File.Exists(path)) File.Delete(path);
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="path">文件相对路径</param>
            public static void DeleteFile(in FileInfo path)
            {
                if (path.Exists) path.Delete();
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="list">文件相对路径</param>
            public static void DeleteFile(in IEnumerable<string> list)
            {
                Parallel.ForEach(list, file => { DeleteFile(file); });
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="list">文件相对路径</param>
            public static void DeleteFile(in IEnumerable<FileInfo> list)
            {
                Parallel.ForEach(list, file => { DeleteFile(file); });
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="list">文件相对路径</param>
            public static void DeleteFile(params FileInfo[] list)
            {
                Parallel.ForEach(list, file => { DeleteFile(file); });
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="list">文件相对路径</param>
            public static void DeleteFile(params string[] list)
            {
                Parallel.ForEach(list, file => { DeleteFile(file); });
            }

            /// <summary>
            /// 删除指定文件夹下 指定类型文件
            /// </summary>
            /// <param name="folder">文件夹路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">查询模式</param>
            public static void DeleteFile(
                in string       folder,
                in string       pattern,
                in SearchOption option = SearchOption.AllDirectories)
            {
                Parallel.ForEach(GetFilesInfo(folder, pattern, option), file => { DeleteFile(file); });
            }

            #endregion
        }

        #endregion
    }
}