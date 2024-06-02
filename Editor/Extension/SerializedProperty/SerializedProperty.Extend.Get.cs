#region namespace

using UnityEditor;

#endregion

namespace AIO.UEditor
{
    public partial class SerializedPropertyExtend
    {
        /// <summary>
        /// Is the value of the `property` the same as the default serialized value for its type?
        /// </summary>
        public static bool IsDefaultValueByType(this SerializedProperty property)
        {
            if (property.hasMultipleDifferentValues)
                return false;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean: return property.boolValue == default;
                case SerializedPropertyType.Float:   return property.floatValue == 0;
                case SerializedPropertyType.String:  return property.stringValue == "";

                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Character:
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.ArraySize:
                    return property.intValue == default;

                case SerializedPropertyType.Vector2: return property.vector2Value == default;
                case SerializedPropertyType.Vector3: return property.vector3Value == default;
                case SerializedPropertyType.Vector4: return property.vector4Value == default;

                case SerializedPropertyType.Quaternion:     return property.quaternionValue == default;
                case SerializedPropertyType.Color:          return property.colorValue == default;
                case SerializedPropertyType.AnimationCurve: return property.animationCurveValue == default;

                case SerializedPropertyType.Rect:   return property.rectValue == default;
                case SerializedPropertyType.Bounds: return property.boundsValue == default;

                case SerializedPropertyType.Vector2Int: return property.vector2IntValue == default;
                case SerializedPropertyType.Vector3Int: return property.vector3IntValue == default;
                case SerializedPropertyType.RectInt:    return property.rectIntValue.Equals(default);
                case SerializedPropertyType.BoundsInt:  return property.boundsIntValue == default;

                case SerializedPropertyType.ObjectReference:  return property.objectReferenceValue == default;
                case SerializedPropertyType.ExposedReference: return property.exposedReferenceValue == default;

                case SerializedPropertyType.FixedBufferSize: return property.fixedBufferSize == default;

                case SerializedPropertyType.Enum: return property.enumValueIndex == default;

                case SerializedPropertyType.Gradient:
                case SerializedPropertyType.Generic:
                default:
                    if (property.isArray) return property.arraySize == default;

                    var depth = property.depth;
                    property = property.Copy();
                    var enterChildren = true;
                    while (property.Next(enterChildren) && property.depth > depth)
                    {
                        enterChildren = false;
                        if (!IsDefaultValueByType(property))
                            return false;
                    }

                    return true;
            }
        }
    }
}