#region

using System.Runtime.CompilerServices;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    partial class VectorExtend
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetX(this ref Vector4 vector, float value)
        {
            vector.x = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetY(this ref Vector4 vector, float value)
        {
            vector.y = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetZ(this ref Vector4 vector, float value)
        {
            vector.z = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetW(this ref Vector4 vector, float value)
        {
            vector.w = value;
        }

        #region XY

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXY(this ref Vector4 vector, float v1, float v2)
        {
            vector.x = v1;
            vector.y = v2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXY(this ref Vector4 vector, in Vector2 value)
        {
            vector.x = value.x;
            vector.y = value.y;
        }

        #endregion

        #region XZ

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXZ(this ref Vector4 vector, float v1, float v2)
        {
            vector.x = v1;
            vector.z = v2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXZ(this ref Vector4 vector, in Vector2 value)
        {
            vector.x = value.x;
            vector.z = value.y;
        }

        #endregion

        #region XW

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXW(this ref Vector4 vector, float v1, float v2)
        {
            vector.x = v1;
            vector.w = v2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXW(this ref Vector4 vector, in Vector2 value)
        {
            vector.x = value.x;
            vector.w = value.y;
        }

        #endregion

        #region YZ

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYZ(this ref Vector4 vector, float v1, float v2)
        {
            vector.y = v1;
            vector.z = v2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYZ(this ref Vector4 vector, in Vector2 value)
        {
            vector.y = value.x;
            vector.z = value.y;
        }

        #endregion

        #region YW

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYW(this ref Vector4 vector, float v1, float v2)
        {
            vector.y = v1;
            vector.w = v2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYW(this ref Vector4 vector, in Vector2 value)
        {
            vector.y = value.x;
            vector.w = value.y;
        }

        #endregion

        #region ZW

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetZW(this ref Vector4 vector, float v1, float v2)
        {
            vector.z = v1;
            vector.w = v2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetZW(this ref Vector4 vector, in Vector2 value)
        {
            vector.z = value.x;
            vector.w = value.y;
        }

        #endregion

        #region XYZ

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXYZ(this ref Vector4 vector, float v1, float v2, float v3)
        {
            vector.x = v1;
            vector.y = v2;
            vector.z = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXYZ(this ref Vector4 vector, Vector2 v12, float v3)
        {
            vector.x = v12.x;
            vector.y = v12.y;
            vector.z = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXYZ(this ref Vector4 vector, Vector3 v123)
        {
            vector.x = v123.x;
            vector.y = v123.y;
            vector.z = v123.z;
        }

        #endregion

        #region XYW

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXYW(this ref Vector4 vector, float v1, float v2, float v3)
        {
            vector.x = v1;
            vector.y = v2;
            vector.w = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXYW(this ref Vector4 vector, Vector2 v12, float v3)
        {
            vector.x = v12.x;
            vector.y = v12.y;
            vector.w = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXYW(this ref Vector4 vector, Vector3 v123)
        {
            vector.x = v123.x;
            vector.y = v123.y;
            vector.w = v123.z;
        }

        #endregion

        #region XZW

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXZW(this ref Vector4 vector, float v1, float v2, float v3)
        {
            vector.x = v1;
            vector.z = v2;
            vector.w = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXZW(this ref Vector4 vector, Vector2 v12, float v3)
        {
            vector.x = v12.x;
            vector.z = v12.y;
            vector.w = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXZW(this ref Vector4 vector, Vector3 v123)
        {
            vector.x = v123.x;
            vector.z = v123.y;
            vector.w = v123.z;
        }

        #endregion

        #region YZW

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYZW(this ref Vector4 vector, float v1, float v2, float v3)
        {
            vector.y = v1;
            vector.z = v2;
            vector.w = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYZW(this ref Vector4 vector, Vector2 v12, float v3)
        {
            vector.y = v12.x;
            vector.z = v12.y;
            vector.w = v3;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYZW(this ref Vector4 vector, Vector3 v123)
        {
            vector.y = v123.x;
            vector.z = v123.y;
            vector.w = v123.z;
        }

        #endregion

        #region XYZW

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in Vector2 value)
        {
            vector.x = value.x;
            vector.y = value.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in Vector2 value1, in Vector2 value2)
        {
            vector.x = value1.x;
            vector.y = value1.y;
            vector.z = value2.x;
            vector.w = value2.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in Vector2 value1, in float z)
        {
            vector.x = value1.x;
            vector.y = value1.y;
            vector.z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in Vector2 value1, in float z, in float w)
        {
            vector.x = value1.x;
            vector.y = value1.y;
            vector.z = z;
            vector.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in float x, in float y, in float z)
        {
            vector.x = x;
            vector.y = y;
            vector.z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in float x, in float y)
        {
            vector.x = x;
            vector.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in float value)
        {
            vector.x =
                vector.y =
                    vector.z =
                        vector.w = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in Vector3 value)
        {
            vector.x = value.x;
            vector.y = value.y;
            vector.z = value.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in Vector3 value, in float w)
        {
            vector.x = value.x;
            vector.y = value.y;
            vector.z = value.z;
            vector.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector4 vector, in Vector4 value)
        {
            vector = value;
        }

        #endregion
    }
}