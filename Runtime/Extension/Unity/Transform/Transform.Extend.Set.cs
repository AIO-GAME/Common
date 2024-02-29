using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIO.UEngine
{
    partial class TransformExtend
    {
        #region Set Pos Local

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalX(this Transform trans, in float v1)
        {
            var gv3 = trans.localPosition;
            gv3.x = v1;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalY(this Transform trans, in float v1)
        {
            var gv3 = trans.localPosition;
            gv3.y = v1;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalZ(this Transform trans, in float v1)
        {
            var gv3 = trans.localPosition;
            gv3.z = v1;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalXY(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.localPosition;
            gv3.x = v1;
            gv3.y = v2;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalXY(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.localPosition;
            gv3.x = v12.x;
            gv3.y = v12.y;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalYZ(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.localPosition;
            gv3.y = v1;
            gv3.z = v2;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalYZ(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.localPosition;
            gv3.y = v12.x;
            gv3.z = v12.y;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalXZ(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.localPosition;
            gv3.x = v1;
            gv3.z = v2;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalXZ(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.localPosition;
            gv3.x = v12.x;
            gv3.z = v12.y;
            trans.localPosition = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalXYZ(this Transform trans, in float v1, in float v2, in float v3)
        {
            trans.localPosition = new Vector3(v1, v2, v3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalXYZ(this Transform trans, in Vector2 v12, float v3)
        {
            trans.localPosition = new Vector3(v12.x, v12.y, v3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosLocalXYZ(this Transform trans, in Vector3 v13)
        {
            trans.localPosition = v13;
        }

        #endregion

        #region Set Pos

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosX(this Transform trans, in float v1)
        {
            var gv3 = trans.position;
            gv3.x = v1;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosY(this Transform trans, in float v1)
        {
            var gv3 = trans.position;
            gv3.y = v1;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosZ(this Transform trans, in float v1)
        {
            var gv3 = trans.position;
            gv3.z = v1;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosXY(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.position;
            gv3.x = v1;
            gv3.y = v2;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosXY(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.position;
            gv3.x = v12.x;
            gv3.y = v12.y;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosYZ(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.position;
            gv3.y = v1;
            gv3.z = v2;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosYZ(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.position;
            gv3.y = v12.x;
            gv3.z = v12.y;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosXZ(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.position;
            gv3.x = v1;
            gv3.z = v2;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosXZ(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.position;
            gv3.x = v12.x;
            gv3.z = v12.y;
            trans.position = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosXYZ(this Transform trans, in float v1, in float v2, in float v3)
        {
            trans.position = new Vector3(v1, v2, v3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosXYZ(this Transform trans, in Vector2 v12, float v3)
        {
            trans.position = new Vector3(v12.x, v12.y, v3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosXYZ(this Transform trans, in Vector3 v13)
        {
            trans.position = v13;
        }

        #endregion

        #region Set Scale

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleX(this Transform trans, in float v1)
        {
            var gv3 = trans.localScale;
            gv3.x = v1;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleY(this Transform trans, in float v1)
        {
            var gv3 = trans.localScale;
            gv3.y = v1;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleZ(this Transform trans, in float v1)
        {
            var gv3 = trans.localScale;
            gv3.z = v1;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXY(this Transform trans, in float v1)
        {
            var gv3 = trans.localScale;
            gv3.x = v1;
            gv3.y = v1;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXY(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.localScale;
            gv3.x = v12.x;
            gv3.y = v12.y;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXY(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.localScale;
            gv3.x = v1;
            gv3.y = v2;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXZ(this Transform trans, in float v1)
        {
            var gv3 = trans.localScale;
            gv3.x = v1;
            gv3.z = v1;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXZ(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.localScale;
            gv3.x = v12.x;
            gv3.z = v12.y;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXZ(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.localScale;
            gv3.x = v1;
            gv3.z = v2;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleYZ(this Transform trans, in float v1)
        {
            var gv3 = trans.localScale;
            gv3.y = v1;
            gv3.z = v1;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleYZ(this Transform trans, in Vector2 v12)
        {
            var gv3 = trans.localScale;
            gv3.y = v12.x;
            gv3.z = v12.y;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleYZ(this Transform trans, in float v1, in float v2)
        {
            var gv3 = trans.localScale;
            gv3.y = v1;
            gv3.z = v2;
            trans.localScale = gv3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXYZ(this Transform trans, in float v1)
        {
            trans.localScale = new Vector3(v1, v1, v1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXYZ(this Transform trans, in float v1, in float v2, in float v3)
        {
            trans.localScale = new Vector3(v1, v2, v3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXYZ(this Transform trans, in Vector2 v12, in float v3)
        {
            trans.localScale = new Vector3(v12.x, v12.y, v3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleXYZ(this Transform trans, in Vector3 v123)
        {
            trans.localScale = new Vector3(v123.x, v123.y, v123.z);
        }

        #endregion

        #region Set Roate

        /// <summary>
        /// 给定一个单位长度的旋转轴(x, y, z)和一个角度θ。对应的四元数为：q=((x,y,z)sinθ2, cosθ2)
        /// </summary>
        public static void SetRotation(this Transform trans, in float x, in float y, in float z)
        {
            trans.rotation = Quaternion.Euler(x, y, z);
        }

        /// <summary>
        /// 给定一个单位长度的旋转轴(x, y, z)和一个角度θ。对应的四元数为：q=((x,y,z)sinθ2, cosθ2)
        /// </summary>
        public static void SetRotation(this Transform trans, in Vector3 v)
        {
            trans.rotation = Quaternion.Euler(v);
        }

        /// <summary>
        /// 给定一个单位长度的旋转轴(x, y, z)和一个角度θ。对应的四元数为：q=((x,y,z)sinθ2, cosθ2)
        /// </summary>
        public static void SetRotation(this Transform trans, in Quaternion v)
        {
            trans.rotation = v;
        }

        #endregion

        /// <summary>
        /// 给定一个单位长度的旋转轴(x, y, z)和一个角度θ。对应的四元数为：q=((x,y,z)sinθ2, cosθ2)
        /// </summary>
        public static void SetLocalRotation(this Transform trans, in float x, in float y, in float z)
        {
            trans.localRotation = Quaternion.Euler(x, y, z);
        }

        /// <summary>
        /// 给定一个单位长度的旋转轴(x, y, z)和一个角度θ。对应的四元数为：q=((x,y,z)sinθ2, cosθ2)
        /// </summary>
        public static void SetLocalRotation(this Transform trans, in Vector3 v)
        {
            trans.localRotation = Quaternion.Euler(v);
        }

        /// <summary>
        /// 给定一个单位长度的旋转轴(x, y, z)和一个角度θ。对应的四元数为：q=((x,y,z)sinθ2, cosθ2)
        /// </summary>
        public static void SetLocalRotation(this Transform trans, in Quaternion v)
        {
            trans.localRotation = v;
        }
    }
}