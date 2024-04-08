using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIO.UEngine
{
    /// <summary>
    /// Game Object Helper
    /// </summary>
    public static partial class GoUtils
    {
        #region Dont Destroy

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject DontDestroyCreate<T>(string name)
        where T : Component
        {
            var go = new GameObject(name);
            go.AddComponent<T>();
            Object.DontDestroyOnLoad(go);
            return go;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject DontDestroyCreate(string name)
        {
            var go = new GameObject(name);
            Object.DontDestroyOnLoad(go);
            return go;
        }

        /// <summary>
        /// 不可销毁物体
        /// </summary>
        public static void DontDestroy(in GameObject go)
        {
            if (go == null) return;
            Object.DontDestroyOnLoad(go);
        }

        /// <summary>
        /// 销毁物体
        /// </summary>
        public static void DontDestroy<T>(in T go)
        where T : Component
        {
            if (go == null) return;
            Object.DontDestroyOnLoad(go);
        }

        #endregion

        public static void DestroyGo(Object obj)
        {
            if (obj == null) return;

            var go = obj as GameObject;
            if (go != null) Object.Destroy(go);
            else
            {
                var c = obj as Component;
                if (c)
                {
                    Object.Destroy(c.gameObject);
                }
            }
        }

        public static void DestroyComp(Object obj)
        {
            if (obj == null) return;

            Object.Destroy(obj as Component);
        }

        public static ICollection<Transform> GetAllChildes(Transform parent, bool recursion = true)
        {
            var r = new List<Transform>();
            FillChild(parent, r, recursion);
            return r;
        }

        private static void FillChild(Transform parent, ICollection<Transform> r, bool recursion)
        {
            for (int i = 0, max = parent.childCount; i < max; ++i)
            {
                var child = parent.GetChild(i);
                r.Add(child);
                if (recursion) FillChild(child, r, true);
            }
        }

        /// <summary>
        /// 获取子节点 (对象) 脚本
        /// </summary>
        /// <typeparam name="T">Component</typeparam>
        /// <param name="go">父对象</param>
        /// <param name="childName">子对象名称</param>
        public static T GetTheChildNodeComponentScripts<T>(in GameObject go, in string childName)
        where T : Component
        {
            var node = go.FindTheChildNode(childName);
            return node.GetComponent<T>();
        }

        /// <summary>
        /// Component.GetComponentInParent 只能获取激活状态的 GameObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T FindComponentInParent<T>(in GameObject go)
        where T : Component
        {
            if (go == null) return null;

            var trans = go.transform;
            var component = trans.GetComponent<T>();

            while (component == null && trans.parent != null)
            {
                trans     = trans.parent;
                component = trans.GetComponent<T>();
            }

            return component;
        }


        /// <summary>
        /// 获取子物体到父物体的路径
        /// </summary>
        /// <param name="child">子物体</param>
        /// <param name="parent">父物体</param>
        /// <param name="connector">隔断字符</param>
        public static string CalTransformPath(in Transform child, in Transform parent, in char connector = '/')
        {
            if (child == null) return string.Empty;

            var r = child.name;
            if (child == parent) return r;

            var t = child.parent;
            while (t != null)
            {
                r = string.Concat(t.name, connector, r);
                if (t == parent) break;
                t = t.parent;
            }

            return r;
        }
    }
}