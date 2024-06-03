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
            public static List<string> FindPaths(
                string          dir,
                SearchOption    op = SearchOption.AllDirectories,
                params string[] searchPatterns)
            {
                dir = dir.Replace('\\', Path.AltDirectorySeparatorChar);
                // 检查文件夹是否存在
                if (!Directory.Exists(dir)) return Pool.List<string>();

                // 设置默认搜索条件
                if (searchPatterns == null || searchPatterns.Length == 0) searchPatterns = new[] { "*" };

                var result = Pool.List<string>();

                // 对每种文件类型进行筛选
                foreach (var pattern in searchPatterns)
                {
                    // 获取符合条件的文件路径
                    var paths = Directory.GetFiles(dir, pattern, op);
                    result.AddRange(paths.Select(path => path.Replace('\\', '/')));
                }

                return result;
            }

            /// <summary>
            /// 查询匹配 返回符合条件的路径
            /// </summary>
            /// <param name="dir">文件夹路径</param>
            /// <param name="op">匹配模式</param>
            /// <param name="searchPatterns">条件 "*value*"</param>
            /// <returns></returns>
            public static List<string> FindPaths(
                string              dir,
                SearchOption        op             = SearchOption.AllDirectories,
                ICollection<string> searchPatterns = null)
            {
                dir = dir.Replace('\\', Path.AltDirectorySeparatorChar);
                if (!Directory.Exists(dir)) return null;
                var result = new List<string>();
                // 设置默认搜索条件
                if (searchPatterns is null || searchPatterns.Count == 0)
                    searchPatterns = new[] { "*" };
                foreach (var pattern in searchPatterns)
                {
                    var paths = Directory.GetFiles(dir, pattern, op);
                    result.AddRange(paths.Select(path => path.Replace('\\', '/')));
                }

                return result;
            }
        }

        #endregion
    }
}