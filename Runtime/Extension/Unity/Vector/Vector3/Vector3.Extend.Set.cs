#region

using System.Runtime.CompilerServices;
using UnityEngine;

#endregion

namespace AIO.UEngine
{
    partial class VectorExtend
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetX(this ref Vector3 vector, float value)
        {
            vector.x = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetY(this ref Vector3 vector, float value)
        {
            vector.y = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetZ(this ref Vector3 vector, float value)
        {
            vector.z = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXZ(this ref Vector3 vector, float value)
        {
            vector.x = vector.z = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXY(this ref Vector3 vector, float value)
        {
            vector.x = vector.y = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYZ(this ref Vector3 vector, float value)
        {
            vector.y = vector.z = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXZ(this ref Vector3 vector, float x, float z)
        {
            vector.x = x;
            vector.z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetXY(this ref Vector3 vector, float x, float y)
        {
            vector.x = x;
            vector.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetYZ(this ref Vector3 vector, float y, float z)
        {
            vector.z = z;
            vector.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector3 vector, float value)
        {
            vector.x = vector.y = vector.z = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector3 vector, float x, float y)
        {
            vector.x = x;
            vector.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector3 vector, Vector3 value)
        {
            vector.x = value.x;
            vector.z = value.y;
            vector.z = value.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector3 vector, Vector2 value)
        {
            vector.x = value.x;
            vector.z = value.y;
        }
    }
}