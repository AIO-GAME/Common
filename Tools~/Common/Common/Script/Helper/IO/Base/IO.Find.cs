#region

using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace AIO
{
    public partial class AHelper
    {
        #region Nested type: IO

        public partial class IO
        {
            /// <summary>
            /// 查询匹配 返回符合条件的路径
            /// </summary>
            /// <param name="dir">文件夹路径</param>
            /// <param name="op">匹配模式</param>
            /// <param name="searchPatterns">条件 "*value*"</param>
            /// <returns></returns>
            public static IReadOnlyList<FileInfo> FindPaths(
                string          dir,
                SearchOption    op = SearchOption.AllDirectories,
                params string[] searchPatterns)
            {
                // 设置默认搜索条件
                if (searchPatterns == null || searchPatterns.Length == 0) searchPatterns = new[] { "*" };
                return FindPaths(dir, op, Pool.List<string>(searchPatterns));
            }

            /// <summary>
            /// 查询匹配 返回符合条件的路径
            /// </summary>
            /// <param name="dir">文件夹路径</param>
            /// <param name="op">匹配模式</param>
            /// <param name="searchPatterns">条件 "*value*"</param>
            /// <returns></returns>
            public static IReadOnlyList<FileInfo> FindPaths(
                string              dir,
                SearchOption        op             = SearchOption.AllDirectories,
                ICollection<string> searchPatterns = null)
            {
                return FindPaths(new DirectoryInfo(dir), op, searchPatterns);
            }

            /// <summary>
            /// 查询匹配 返回符合条件的路径
            /// </summary>
            /// <param name="dir">文件夹路径</param>
            /// <param name="op">匹配模式</param>
            /// <param name="searchPatterns">条件 "*value*"</param>
            /// <returns></returns>
            public static IReadOnlyList<FileInfo> FindPaths(
                DirectoryInfo       dir,
                SearchOption        op             = SearchOption.AllDirectories,
                ICollection<string> searchPatterns = null)
            {
                if (!dir.Exists) return null;
                var result                                                              = new List<FileInfo>();
                if (searchPatterns is null || searchPatterns.Count == 0) searchPatterns = new[] { "*" };
                foreach (var pattern in searchPatterns) result.AddRange(dir.GetFiles(pattern, op));
                return result;
            }
        }

        #endregion
    }
}