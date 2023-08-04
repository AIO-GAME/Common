/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2023-06-04
|*|E-Mail:     |*| 1398581458@qq.com
|*|============|*/

using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class UtilsEditor
    {
        public partial class Asset
        {
            /// <summary>
            /// 获取资源的相对路径
            /// </summary>
            public static string GetAssetPath(Object assetObject)
            {
                return AssetDatabase.GetAssetPath(assetObject);
            }

            /// <summary>
            /// 获取选中资源信息
            /// </summary>
            /// <returns>选中信息</returns>
            public static string GetSelectAsText()
            {
                if (Selection.activeObject is TextAsset asset)
                    return asset.text;

                var path = AssetDatabase.GetAssetPath(Selection.activeObject);
                if (string.IsNullOrEmpty(path)) return null;
                return File.ReadAllText(path);
            }

            /// <summary>
            /// 获取选中资源路径
            /// </summary>
            /// <returns></returns>
            public static string GetSelectAssetPath()
            {
                return AssetDatabase.GetAssetPath(Selection.activeObject);
            }

            /// <summary>
            /// 获取多个选中路径
            /// </summary>
            /// <returns></returns>
            public static string[] GetSelectPaths()
            {
                var r = Selection.objects.Select(AssetDatabase.GetAssetPath).ToList();

                foreach (var obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
                {
                    var assets = AssetDatabase.GetAssetPath(obj);
                    if (!string.IsNullOrEmpty(assets) && File.Exists(assets))
                    {
                        assets = System.IO.Path.GetDirectoryName(assets);
                    }

                    r.Remove(assets);
                    r.Add(assets);
                }

                return r.ToArray();
            }
        }
    }
}
