using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO.UEngine
{
    using Object = Object;

    public static partial class GameObjectExtend
    {
        #region Destroy

        public static void Destroy<T>(this T obj, in float tiems) where T : Object
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                Object.Destroy(obj, tiems);
            else Object.DestroyImmediate(obj);
#else
            Object.Destroy(obj, tiems);
#endif
        }

        public static void Destroy<T>(this T obj) where T : Object
        {
#if UNITY_EDITOR
            if (Application.isPlaying) Object.Destroy(obj);
            else Object.DestroyImmediate(obj);
#else
            Object.Destroy(obj);
#endif
        }

        public static void DestroyChlids(this GameObject obj)
        {
            if (obj == null) return;
            while (obj.transform.childCount > 0)
            {
                //依次从第一个开始销毁 如果第一个子物体销毁完成 则销毁第二个
                if (obj.transform.GetChild(0).childCount == 0)
                    obj.transform.GetChild(0).Destroy();
                else obj.transform.GetChild(0).DestroyChildes();
            }
        }

        #endregion

        #region Clone

        public static GameObject Clone(this GameObject obj)
        {
            return Object.Instantiate(obj);
        }

        public static GameObject Clone(this GameObject obj, in Transform transform)
        {
            return Object.Instantiate(obj, transform);
        }

        public static GameObject Clone(this GameObject obj, in Vector3 postion, in Quaternion rotation,
            in Transform trans)
        {
            return Object.Instantiate(obj, postion, rotation, trans);
        }

        public static T Clone<T>(this T obj) where T : Object
        {
            return Object.Instantiate(obj);
        }

        public static T Clone<T>(this T obj, in Transform trans) where T : Object
        {
            return Object.Instantiate(obj, trans);
        }

        public static T Clone<T>(this T obj, in Vector3 postion, in Quaternion rotation, in Transform trans)
            where T : Object
        {
            return Object.Instantiate(obj, postion, rotation, trans);
        }

        #endregion

        #region Remove Component

        public static void RemoveComponent<T>(this GameObject obj) where T : Component
        {
            Destroy(obj.GetComponent<T>());
        }

        public static void RemoveComponent(this GameObject obj, in string comp)
        {
            Destroy(obj.GetComponent(comp));
        }

        #endregion

        /// <summary>
        /// 给子节点添加脚本
        /// </summary>
        /// <typeparam name="T">Component</typeparam>
        /// <param name="obj">父对象</param>
        /// <param name="childName">子对象名称</param>
        public static T AddChildNodeComponent<T>(this GameObject obj, in string childName) where T : Component
        {
            //查找特定节点结果  查找特定子节点
            var transform = obj.FindTheChildNode(childName);
            //如果查找成功,则考虑如果已经有相同的脚本了,则先删除,否则直接添加。
            if (transform == null) return null; //如果查找不成功,返回Null.
            //如果已经有相同的脚本了,则先删除
            var componentScriptsArray = transform.GetComponents<T>();
            foreach (var component in componentScriptsArray)
            {
                if (!component.Equals(null))
                    component.Destroy();
            }

            return transform.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 查找子节点对象   内部使用 "递归算法"
        /// </summary>
        /// <param name="obj">父对象</param>
        /// <param name="childName">查找的子对象名称</param>
        public static Transform FindTheChildNode(this GameObject obj, in string childName)
        {
            var searchTrans = obj.transform.Find(childName); //查找结果
            if (searchTrans.Equals(null))
            {
                foreach (Transform trans in obj.transform)
                {
                    searchTrans = FindTheChildNode(trans.gameObject, childName);
                    if (searchTrans != null) return searchTrans;
                }
            }

            return searchTrans;
        }

        public static void Foreach(this GameObject obj, in bool r, in Action<GameObject> act)
        {
            var ts = obj.transform;
            for (int i = 0, max = ts.childCount; i < max; ++i)
            {
                var child = ts.GetChild(i).gameObject;
                if (r) child.Foreach(r, act);
                act(child);
            }
        }
    }
}