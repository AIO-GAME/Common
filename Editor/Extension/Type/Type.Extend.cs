#region namespace

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 类扩展
    /// </summary>
    public static class TypeExtend
    {
        /// <summary>
        /// 返回表示指定“类型”字段的SerializedPropertyType。
        /// </summary>
        public static SerializedPropertyType GetPropertyType(this Type type)
        {
            // Primitives.

            if (type == typeof(bool))
                return SerializedPropertyType.Boolean;

            if (type == typeof(int))
                return SerializedPropertyType.Integer;

            if (type == typeof(float))
                return SerializedPropertyType.Float;

            if (type == typeof(string))
                return SerializedPropertyType.String;

            if (type == typeof(LayerMask))
                return SerializedPropertyType.LayerMask;

            // Vectors.

            if (type == typeof(Vector2))
                return SerializedPropertyType.Vector2;
            if (type == typeof(Vector3))
                return SerializedPropertyType.Vector3;
            if (type == typeof(Vector4))
                return SerializedPropertyType.Vector4;

            if (type == typeof(Quaternion))
                return SerializedPropertyType.Quaternion;

            // Other.

            if (type == typeof(Color) || type == typeof(Color32))
                return SerializedPropertyType.Color;
            if (type == typeof(Gradient))
                return SerializedPropertyType.Gradient;

            if (type == typeof(Rect))
                return SerializedPropertyType.Rect;
            if (type == typeof(Bounds))
                return SerializedPropertyType.Bounds;

            if (type == typeof(AnimationCurve))
                return SerializedPropertyType.AnimationCurve;

            // Int Variants.

            if (type == typeof(Vector2Int))
                return SerializedPropertyType.Vector2Int;
            if (type == typeof(Vector3Int))
                return SerializedPropertyType.Vector3Int;
            if (type == typeof(RectInt))
                return SerializedPropertyType.RectInt;
            if (type == typeof(BoundsInt))
                return SerializedPropertyType.BoundsInt;

            // Special.
            if (typeof(Object).IsAssignableFrom(type))
                return SerializedPropertyType.ObjectReference;

            if (type.IsEnum)
                return SerializedPropertyType.Enum;

            return SerializedPropertyType.Generic;
        }
    }
}