using System;
using UnityEngine;

namespace AIO.UEngine
{
    partial class GameObjectExtend
    {
        /// <summary>
        /// 获取子节点 (对象) 脚本
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="go">父对象</param>
        /// <param name="childName">子对象名称</param>
        public static T GetTheChildNodeComponentScripts<T>(this GameObject go, in string childName) where T : Component
        {
            return go.FindTheChildNode(childName).GetComponent<T>(); //查找特定子节点
        }

        /// <summary>
        /// 获取或添加组件
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T r = go.GetComponent<T>();
            if (r == null) r = go.AddComponent<T>();
            return r;
        }

        public static T GetComponentInParent<T>(this GameObject go, in bool includeInactive) where T : Component
        {
            if (go.activeInHierarchy && !includeInactive) return go.GetComponentInParent<T>();

            var tf = go.transform;
            while (tf != null)
            {
                if (!tf.gameObject.activeSelf && !includeInactive) break;

                var r = tf.GetComponent<T>();
                if (r != null) return r;
                tf = tf.parent;
            }

            return default;
        }

        public static Component GetComponentInParent(this GameObject go, in Type type, in bool includeInactive)
        {
            if (go.activeInHierarchy && !includeInactive) return go.GetComponentInParent(type);

            var tf = go.transform;
            while (tf != null)
            {
                if (!tf.gameObject.activeSelf && !includeInactive)
                {
                    break;
                }

                var r = tf.GetComponent(type);
                if (r != null) return r;
                tf = tf.parent;
            }

            return null;
        }
    }
}