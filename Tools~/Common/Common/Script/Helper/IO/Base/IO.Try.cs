/*|============|*|
|*|Author:     |*| USER
|*|Date:       |*| 2024-01-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AIO
{
    public partial class AHelper
    {
        public partial class IO
        {
            #region Delete File

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="path">文件相对路径</param>
            public static bool TryDeleteFile(in string path)
            {
                if (!File.Exists(path)) return false;
                File.Delete(path);
                return true;
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="path">文件相对路径</param>
            public static bool TryDeleteFile(in FileInfo path)
            {
                if (!path.Exists) return false;
                path.Delete();
                return true;
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="list">文件相对路径</param>
            public static IDictionary<string, bool> TryDeleteFile(in IEnumerable<string> list)
            {
                var enumerable = Pool.Dictionary<string, bool>();
                Parallel.ForEach(list, file => { enumerable[file] = TryDeleteFile(file); });
                return enumerable;
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="list">文件相对路径</param>
            public static IDictionary<string, bool> TryDeleteFile(in IEnumerable<FileInfo> list)
            {
                var enumerable = Pool.Dictionary<string, bool>();
                Parallel.ForEach(list, file => { enumerable[file.FullName] = TryDeleteFile(file); });
                return enumerable;
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="fileInfo">文件路径</param>
            /// <param name="list">文件相对路径</param>
            public static IDictionary<string, bool> TryDeleteFile(FileInfo fileInfo, params FileInfo[] list)
            {
                var enumerable = Pool.Dictionary<string, bool>();
                enumerable[fileInfo.FullName] = TryDeleteFile(fileInfo);
                Parallel.ForEach(list, file => { enumerable[file.FullName] = TryDeleteFile(file); });
                return enumerable;
            }

            /// <summary>
            /// 删除文件
            /// </summary>
            /// <param name="fileInfo">文件路径</param>
            /// <param name="list">文件相对路径</param>
            public static IDictionary<string, bool> TryDeleteFile(string fileInfo, params string[] list)
            {
                var enumerable = Pool.Dictionary<string, bool>();
                enumerable[fileInfo] = TryDeleteFile(fileInfo);
                Parallel.ForEach(list, file => { enumerable[file] = TryDeleteFile(file); });
                return enumerable;
            }

            /// <summary>
            /// 删除指定文件夹下 指定类型文件
            /// </summary>
            /// <param name="folder">文件夹路径</param>
            /// <param name="pattern">匹配模式</param>
            /// <param name="option">查询模式</param>
            public static IDictionary<string, bool> TryDeleteFile(
                in string folder,
                in string pattern,
                in SearchOption option = SearchOption.AllDirectories)
            {
                var enumerable = Pool.Dictionary<string, bool>();
                Parallel.ForEach(GetFilesInfo(folder, pattern, option),
                    file => { enumerable[file.FullName] = TryDeleteFile(file); });
                return enumerable;
            }

            #endregion

            /// <summary>
            /// 删除文件夹
            /// </summary>
            public static bool TryDeleteDir(
                in DirectoryInfo directory,
                in SearchOption option = SearchOption.AllDirectories,
                in bool isAll = false)
            {
                if (!directory.Exists) return false;
                try
                {
                    if (isAll) Parallel.ForEach(directory.GetFiles("*", option), file => { DeleteFile(file); });
                    directory.Delete(isAll);
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }

            /// <summary>
            /// 删除文件夹
            /// </summary>
            public static bool TryDeleteDir(
                in string director,
                in SearchOption option = SearchOption.AllDirectories,
                in bool isAll = false)
            {
                return TryDeleteDir(new DirectoryInfo(director), option, isAll);
            }
        }
    }
}