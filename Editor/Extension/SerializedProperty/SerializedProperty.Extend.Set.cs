namespace AIO.UEditor
{
    using UnityEditor;

    public partial class SerializedPropertyExtend
    {
        /// <summary>
        /// Resets the value of the <see cref="SerializedProperty"/> to the default value of its type and all its field
        /// types, ignoring values set by constructors or field initializers.
        /// </summary>
        /// <remarks>
        /// If you want to run constructors and field initializers, you can call
        /// </remarks>
        public static void ResetValue(this SerializedProperty property, string undoName = "Inspector")
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    property.boolValue = default;
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = default;
                    break;
                case SerializedPropertyType.String:
                    property.stringValue = "";
                    break;

                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Character:
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.ArraySize:
                    property.intValue = default;
                    break;

                case SerializedPropertyType.Vector2:
                    property.vector2Value = default;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = default;
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = default;
                    break;

                case SerializedPropertyType.Quaternion:
                    property.quaternionValue = default;
                    break;
                case SerializedPropertyType.Color:
                    property.colorValue = default;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    property.animationCurveValue = default;
                    break;

                case SerializedPropertyType.Rect:
                    property.rectValue = default;
                    break;
                case SerializedPropertyType.Bounds:
                    property.boundsValue = default;
                    break;

                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = default;
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = default;
                    break;
                case SerializedPropertyType.RectInt:
                    property.rectIntValue = default;
                    break;
                case SerializedPropertyType.BoundsInt:
                    property.boundsIntValue = default;
                    break;

                case SerializedPropertyType.ObjectReference:
                    property.objectReferenceValue = default;
                    break;
                case SerializedPropertyType.ExposedReference:
                    property.exposedReferenceValue = default;
                    break;

                case SerializedPropertyType.Enum:
                    property.enumValueIndex = default;
                    break;

                case SerializedPropertyType.Gradient:
                case SerializedPropertyType.FixedBufferSize:
                case SerializedPropertyType.Generic:
                default:
                    if (property.isArray)
                    {
                        property.arraySize = default;
                        break;
                    }

                    var depth = property.depth;
                    property = property.Copy();
                    var enterChildren = true;
                    while (property.Next(enterChildren) && property.depth > depth)
                    {
                        enterChildren = false;
                        ResetValue(property);
                    }

                    break;
            }
        }
    }
}
