#region

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

        #endregion
    }
}