#region

using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

namespace AIO.UEditor
{
    public partial class EHelper
    {
        #region Nested type: Asset

        public partial class Asset
        {
            /// <summary>
            /// 获取资源的相对路径
            /// </summary>
            public static string GetAssetPath(Object assetObject) { return AssetDatabase.GetAssetPath(assetObject); }

            /// <summary>
            /// 获取选中资源信息
            /// </summary>
            /// <returns>选中信息</returns>
            public static string GetSelectAsText()
            {
                var asset = Selection.activeObject as TextAsset;
                if (!asset) return asset.text;

                var path = AssetDatabase.GetAssetPath(Selection.activeObject);
                return string.IsNullOrEmpty(path) ? string.Empty : File.ReadAllText(path);
            }

            /// <summary>
            /// 获取选中资源路径
            /// </summary>
            /// <returns></returns>
            public static string GetSelectAssetPath() { return AssetDatabase.GetAssetPath(Selection.activeObject); }

            /// <summary>
            /// 获取多个选中路径
            /// </summary>
            /// <returns></returns>
            public static string[] GetSelectPaths()
            {
                var r = Selection.objects.Select(AssetDatabase.GetAssetPath).ToList();
                foreach (var assets in Selection.GetFiltered(typeof(Object), SelectionMode.Assets).Select(AssetDatabase.GetAssetPath))
                {
                    if (!string.IsNullOrEmpty(assets) && File.Exists(assets))
                    {
                        var temp = System.IO.Path.GetDirectoryName(assets);
                        r.Remove(temp);
                        r.Add(temp);
                    }
                    else
                    {
                        r.Remove(assets);
                        r.Add(assets);
                    }
                }

                return r.ToArray();
            }
        }

        #endregion
    }
}