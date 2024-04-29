#region

using System;
using System.Linq;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    public static class ComponentExtend
    {
        public static void DestroyAllChildes<T>(this T component)
        where T : Component
        {
            while (component.transform.childCount <= 0)
                if (component.transform.GetChild(0).childCount == 0)
                    component.transform.GetChild(0).gameObject.Destroy();
                else component.transform.GetChild(0).DestroyChildes();
        }

        public static T GetComponentInParent<T>(this T component, in bool includeInactive)
        where T : Component
        {
            return component.gameObject.GetComponentInParent<T>(includeInactive);
        }

        public static T1 GetComponentInChildren<T1, T2>(this Component component, in bool includeInactive)
        where T1 : Component where T2 : Component
        {
            var rs = component.GetComponentsInChildren<T1>(includeInactive);
            return (from r in rs
                    let temp = r.GetComponentInParent<T2>()
                    where temp != null && temp.gameObject == component.gameObject
                    select r).FirstOrDefault();
        }

        public static T1[] GetComponentInChildes<T1, T2>(this Component component, in bool includeInactive)
        where T1 : Component where T2 : Component
        {
            var rs = component.GetComponentsInChildren<T1>(includeInactive);
            var mySelf = component as T2;
            return (from item in rs
                    where item != null
                    let t2 = item.GetComponentInParent<T2>()
                    where t2 != null && t2 == mySelf
                    select item).ToArray();
        }


        /// <summary>
        /// 判断组件是否在指定距离内
        /// </summary>
        /// <returns>Ture:满足 False:不满足</returns>
        public static bool ConditionDistance(this Component score, Component target, float distances)
        {
            return Vector3.Distance(score.transform.position, target.transform.position) <= distances;
        }

        #region Set Layer

        public static void SetLayer<T>(this T component, in int layer)
        where T : Component
        {
            component.gameObject.layer = layer;
        }

        public static void SetLayer<T>(this T component, in Enum layer)
        where T : Component
        {
            component.gameObject.layer = layer.GetHashCode();
        }

        public static void SetLayerAll<T>(this T component, in int layer)
        where T : Component
        {
            component.gameObject.layer = layer;
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null || children.Length == 0) return;
            foreach (var child in children) child.gameObject.layer = layer;
        }

        public static void SetLayerAll<T>(this T component, in Enum layer)
        where T : Component
        {
            var code = layer.GetHashCode();
            component.gameObject.layer = code;
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null || children.Length == 0) return;
            foreach (var child in children) child.gameObject.layer = code;
        }

        public static void SetLayerChildes<T>(this T component, in int layer)
        where T : Component
        {
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null || children.Length == 0) return;
            foreach (var child in children) child.gameObject.layer = layer;
        }

        public static void SetLayerChildes<T>(this T component, in Enum layer)
        where T : Component
        {
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null) return;
            var code = layer.GetHashCode();
            foreach (var child in children) child.gameObject.layer = code;
        }

        #endregion

        #region SetTag

        public static void SetTag<T>(this T trans, in string tag)
        where T : Component
        {
            trans.tag = tag;
        }

        public static void SetTagAll<T>(this T trans, in string tag)
        where T : Component
        {
            trans.tag = tag;
            foreach (Transform item in trans.transform)
            {
                item.tag = tag;
                if (item.childCount > 0) SetTagAll(item, tag);
            }
        }

        public static void SetTagChildes<T>(this T trans, in string tag)
        where T : Component
        {
            foreach (Transform item in trans.transform)
            {
                item.tag = tag;
                if (item.childCount > 0) SetTagAll(item, tag);
            }
        }

        #endregion
    }
}