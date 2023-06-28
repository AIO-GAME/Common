/*|============|*|
|*|Author:     |*| xinan                
|*|Date:       |*| 2023-06-04               
|*|E-Mail:     |*| 1398581458@qq.com     
|*|============|*/

using UnityEditor;
using UnityEngine;

namespace UnityEditor
{
    public partial class UtilsEditor
    {
        public partial class Asset
        {
            /// <summary>
            /// 保存所有资源
            /// </summary>
            public static void ApplyAssets()
            {
                AssetDatabase.SaveAssets();
            }

            /// <summary>
            /// 保存预制件修改
            /// </summary>
            public static void ApplyPrefab(GameObject go)
            {
                if (go == null) return;
                var prefab = PrefabUtility.GetCorrespondingObjectFromSource(go);
                if (prefab != null) PrefabUtility.SavePrefabAsset(prefab);
            }
        }
    }
}