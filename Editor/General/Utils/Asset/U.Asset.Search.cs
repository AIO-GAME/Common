#region

using System.Collections.Generic;
using System.Linq;
using UnityEditor;

#endregion

namespace AIO.UEditor
{
    public partial class EHelper
    {
        #region Nested type: Asset

        public partial class Asset
        {
            /// <summary>
            /// 遍历查找符合的文件
            /// </summary>
            /// <param name="filter">过滤器</param>
            /// <param name="folder">文件夹</param>
            /// <returns>路径</returns>
            public static IList<string> SearchDirs(string filter, string folder)
            {
                return AssetDatabase.FindAssets(filter, new[] { folder }).Select(AssetDatabase.GUIDToAssetPath).ToList();
            }

            /// <summary>
            /// 遍历查找符合的文件
            /// </summary>
            /// <param name="filter">过滤器</param>
            /// <param name="folder">文件夹</param>
            /// <returns>路径</returns>
            public static IList<string> SearchDirs(string filter, string[] folder)
            {
                return AssetDatabase.FindAssets(filter, folder).Select(AssetDatabase.GUIDToAssetPath).ToList();
            }
        }

        #endregion
    }
}