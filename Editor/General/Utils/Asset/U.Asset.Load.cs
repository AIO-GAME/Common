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
            /// 加载 并 实例化 资源
            /// </summary>
            /// <param name="assetPath">资源路径</param>
            /// <returns>实体</returns>
            public static GameObject LoadAndInstance(string assetPath)
            {
                return LoadAndInstance(assetPath, null, Vector3.zero, Quaternion.identity, Vector3.one);
            }

            /// <summary>
            /// 加载 并 实例化 资源
            /// </summary>
            /// <param name="assetPath">资源路径</param>
            /// <param name="parent">父节点</param>
            /// <returns>实体</returns>
            public static GameObject LoadAndInstance(string assetPath, Transform parent)
            {
                return LoadAndInstance(assetPath, parent, Vector3.zero, Quaternion.identity, Vector3.one);
            }

            /// <summary>
            /// 加载 并 实例化 资源
            /// </summary>
            /// <param name="assetPath">资源路径</param>
            /// <param name="parent">父节点</param>
            /// <param name="pos">实例化坐标</param>
            /// <returns>实体</returns>
            public static GameObject LoadAndInstance(string assetPath, Transform parent, Vector3 pos)
            {
                return LoadAndInstance(assetPath, parent, pos, Quaternion.identity, Vector3.one);
            }

            /// <summary>
            /// 加载 并 实例化 资源
            /// </summary>
            /// <param name="assetPath">资源路径</param>
            /// <param name="parent">父节点</param>
            /// <param name="pos">实例化坐标</param>
            /// <param name="rot">旋转信息</param>
            /// <returns>实体</returns>
            public static GameObject LoadAndInstance(string assetPath, Transform parent, Vector3 pos, Quaternion rot)
            {
                return LoadAndInstance(assetPath, parent, pos, rot, Vector3.one);
            }

            /// <summary>
            /// 加载 并 实例化 资源
            /// </summary>
            /// <param name="assetPath">资源路径</param>
            /// <param name="parent">父物体</param>
            /// <param name="pos">位置</param>
            /// <param name="rot">旋转</param>
            /// <param name="scale">缩放</param>
            /// <returns>实体</returns>
            public static GameObject LoadAndInstance(string assetPath, Transform parent, Vector3 pos, Quaternion rot, Vector3 scale)
            {
                var o = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if (o != null)
                {
                    var go = Object.Instantiate(o, parent, true) as GameObject;
                    if (go != null)
                    {
                        go.name                    = o.name;
                        go.transform.localPosition = pos;
                        go.transform.localRotation = rot;
                        go.transform.localScale    = scale;
                        return go;
                    }
                }

                return null;
            }

            /// <summary>
            /// 加载 并 实例化 资源
            /// </summary>
            /// <param name="assetPath">资源路径</param>
            /// <param name="parent">父物体</param>
            /// <returns>实体</returns>
            public static T LoadAndInstance<T>(string assetPath, Transform parent)
            where T : Component
            {
                var r = LoadAndInstance(assetPath, parent);
                if (r != null) return r.GetComponent<T>() ?? r.AddComponent<T>();
                return null;
            }
        }

        #endregion
    }
}