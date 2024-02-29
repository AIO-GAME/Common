using System;
using UnityEngine;

namespace AIO.UEngine
{
    partial class GameObjectExtend
    {
        #region Set Parent

        /// <summary>
        /// 设置父物体位置
        /// </summary>
        /// <param name="go"></param>
        /// <param name="target">目标</param>
        /// <param name="worldPositionStays">世界空间</param>
        public static void SetParent(this GameObject go, in Transform target, in bool worldPositionStays = true)
        {
            go.transform.SetParent(target, worldPositionStays);
        }

        /// <summary>
        /// 设置父物体位置
        /// </summary>
        /// <param name="go"></param>
        /// <param name="target">目标</param>
        /// <param name="worldPositionStays">世界空间</param>
        public static void SetParent(this GameObject go, in GameObject target, in bool worldPositionStays = true)
        {
            go.transform.SetParent(target.transform, worldPositionStays);
        }

        /// <summary>
        /// 设置父物体位置
        /// </summary>
        /// <param name="go"></param>
        /// <param name="target">目标</param>
        /// <param name="worldPositionStays">世界空间</param>
        public static void SetParent(this GameObject go, in Component target, in bool worldPositionStays = true)
        {
            go.transform.SetParent(target.transform, worldPositionStays);
        }

        #endregion

        #region Set Pos Local

        public static void SetPosLocalX(this GameObject go, in float v1)
        {
            var gv3 = go.transform.localPosition;
            gv3.x = v1;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalY(this GameObject go, in float v1)
        {
            var gv3 = go.transform.localPosition;
            gv3.y = v1;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalZ(this GameObject go, in float v1)
        {
            var gv3 = go.transform.localPosition;
            gv3.z = v1;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalXY(this GameObject go, in float v1, in float v2)
        {
            var gv3 = go.transform.localPosition;
            gv3.x = v1;
            gv3.y = v2;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalXY(this GameObject go, in Vector2 v12)
        {
            var gv3 = go.transform.localPosition;
            gv3.x = v12.x;
            gv3.y = v12.y;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalYZ(this GameObject go, in float v1, in float v2)
        {
            var gv3 = go.transform.localPosition;
            gv3.y = v1;
            gv3.z = v2;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalYZ(this GameObject go, in Vector2 v12)
        {
            var gv3 = go.transform.localPosition;
            gv3.y = v12.x;
            gv3.z = v12.y;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalXZ(this GameObject go, in float v1, in float v2)
        {
            var gv3 = go.transform.localPosition;
            gv3.x = v1;
            gv3.z = v2;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalXZ(this GameObject go, in Vector2 v12)
        {
            var gv3 = go.transform.localPosition;
            gv3.x = v12.x;
            gv3.z = v12.y;
            go.transform.localPosition = gv3;
        }


        public static void SetPosLocalXYZ(this GameObject go, in float v1, in float v2, in float v3)
        {
            go.transform.localPosition = new Vector3(v1, v2, v3);
        }


        public static void SetPosLocalXYZ(this GameObject go, in Vector2 v12, float v3)
        {
            go.transform.localPosition = new Vector3(v12.x, v12.y, v3);
        }


        public static void SetPosLocalXYZ(this GameObject go, in Vector3 v13)
        {
            go.transform.localPosition = v13;
        }

        #endregion

        #region Set Pos

        public static void SetPosX(this GameObject go, in float v1)
        {
            var gv3 = go.transform.position;
            gv3.x = v1;
            go.transform.position = gv3;
        }


        public static void SetPosY(this GameObject go, in float v1)
        {
            var gv3 = go.transform.position;
            gv3.y = v1;
            go.transform.position = gv3;
        }


        public static void SetPosZ(this GameObject go, in float v1)
        {
            var gv3 = go.transform.position;
            gv3.z = v1;
            go.transform.position = gv3;
        }


        public static void SetPosXY(this GameObject go, in float v1, in float v2)
        {
            var gv3 = go.transform.position;
            gv3.x = v1;
            gv3.y = v2;
            go.transform.position = gv3;
        }


        public static void SetPosXY(this GameObject go, in Vector2 v12)
        {
            var gv3 = go.transform.position;
            gv3.x = v12.x;
            gv3.y = v12.y;
            go.transform.position = gv3;
        }


        public static void SetPosYZ(this GameObject go, in float v1, in float v2)
        {
            var gv3 = go.transform.position;
            gv3.y = v1;
            gv3.z = v2;
            go.transform.position = gv3;
        }


        public static void SetPosYZ(this GameObject go, in Vector2 v12)
        {
            var gv3 = go.transform.position;
            gv3.y = v12.x;
            gv3.z = v12.y;
            go.transform.position = gv3;
        }


        public static void SetPosXZ(this GameObject go, in float v1, in float v2)
        {
            var gv3 = go.transform.position;
            gv3.x = v1;
            gv3.z = v2;
            go.transform.position = gv3;
        }


        public static void SetPosXZ(this GameObject go, in Vector2 v12)
        {
            var gv3 = go.transform.position;
            gv3.x = v12.x;
            gv3.z = v12.y;
            go.transform.position = gv3;
        }


        public static void SetPosXYZ(this GameObject go, in float v1, in float v2, in float v3)
        {
            go.transform.position = new Vector3(v1, v2, v3);
        }


        public static void SetPosXYZ(this GameObject go, in Vector2 v12, float v3)
        {
            go.transform.position = new Vector3(v12.x, v12.y, v3);
        }


        public static void SetPosXYZ(this GameObject go, in Vector3 v13)
        {
            go.transform.position = v13;
        }

        #endregion

        #region Set Layer

        /// <summary>
        /// 设置自己的所属层级
        /// </summary>
        public static void SetLayer(this GameObject component, Enum layer)
        {
            component.layer = layer.GetHashCode();
        }

        /// <summary>
        /// 设置自己的所属层级
        /// </summary>
        public static void SetLayer(this GameObject component, int layer)
        {
            component.layer = layer;
        }

        /// <summary>
        /// 设置自己及其子对象的所属层级
        /// </summary>
        public static void SetLayerAll(this GameObject component, int layer)
        {
            component.layer = layer;
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null) return;
            foreach (var child in children) child.gameObject.layer = layer;
        }


        /// <summary>
        /// 设置自己及其子对象的所属层级
        /// </summary>
        public static void SetLayerAll(this GameObject component, Enum layer)
        {
            component.layer = layer.GetHashCode();
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null) return;
            foreach (var child in children) child.gameObject.layer = component.layer;
        }

        /// <summary>
        /// 设置全部子对象的所属层级
        /// </summary>
        public static void SetLayerChilds(this GameObject component, Enum layer)
        {
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null) return;
            var code = layer.GetHashCode();
            foreach (var child in children) child.gameObject.layer = code;
        }

        /// <summary>
        /// 设置全部子对象的所属层级
        /// </summary>
        public static void SetLayerChilds(this GameObject component, int layer)
        {
            var children = component.GetComponentsInChildren<Transform>(true);
            if (children == null) return;
            foreach (var child in children) child.gameObject.layer = layer;
        }

        #endregion

        public static void SetTag(this GameObject trans, in string tag)
        {
            trans.tag = tag;
        }

        public static void SetTagAll(this GameObject trans, in string tag)
        {
            trans.tag = tag;
            foreach (Transform item in trans.transform)
            {
                item.tag = tag;
                if (item.childCount > 0) item.SetTagAll(tag);
            }
        }

        public static void SetTagChildes(this GameObject trans, in string tag)
        {
            foreach (Transform item in trans.transform)
            {
                item.tag = tag;
                if (item.childCount > 0) item.SetTagAll(tag);
            }
        }
    }
}