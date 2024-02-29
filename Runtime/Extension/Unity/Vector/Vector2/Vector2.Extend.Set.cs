namespace AIO.UEngine
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    partial class VectorExtend
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetX(this ref Vector2 vector, in float value)
        {
            vector.x = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetY(this ref Vector2 vector, in float value)
        {
            vector.y = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector2 vector, in Vector2 value)
        {
            vector = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Set(this ref Vector2 vector, in float value)
        {
            vector.x = vector.y = value;
        }
    }
}