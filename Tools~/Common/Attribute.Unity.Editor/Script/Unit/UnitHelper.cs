using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIO
{
    internal static class UnitHelper
    {
        /// <summary>
        /// Sets the value of the specified <see cref="SerializedProperty"/>.
        /// </summary>
        public static void SetValue(this SerializedProperty property, object targetObject, object value)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    property.boolValue = (bool)value;
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = (float)value;
                    break;
                case SerializedPropertyType.String:
                    property.stringValue = (string)value;
                    break;

                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Character:
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.ArraySize:
                    property.intValue = (int)value;
                    break;

                case SerializedPropertyType.Vector2:
                    property.vector2Value = (Vector2)value;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = (Vector3)value;
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = (Vector4)value;
                    break;

                case SerializedPropertyType.Quaternion:
                    property.quaternionValue = (Quaternion)value;
                    break;
                case SerializedPropertyType.Color:
                    property.colorValue = (Color)value;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    property.animationCurveValue = (AnimationCurve)value;
                    break;

                case SerializedPropertyType.Rect:
                    property.rectValue = (Rect)value;
                    break;
                case SerializedPropertyType.Bounds:
                    property.boundsValue = (Bounds)value;
                    break;

                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = (Vector2Int)value;
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = (Vector3Int)value;
                    break;
                case SerializedPropertyType.RectInt:
                    property.rectIntValue = (RectInt)value;
                    break;
                case SerializedPropertyType.BoundsInt:
                    property.boundsIntValue = (BoundsInt)value;
                    break;

                case SerializedPropertyType.ObjectReference:
                    property.objectReferenceValue = (Object)value;
                    break;
                case SerializedPropertyType.ExposedReference:
                    property.exposedReferenceValue = (Object)value;
                    break;

                case SerializedPropertyType.FixedBufferSize:
                    throw new InvalidOperationException(
                        $"{nameof(SetValue)} failed: {nameof(SerializedProperty)}.{nameof(SerializedProperty.fixedBufferSize)} is read-only.");

                case SerializedPropertyType.Gradient:
                    GradientValue.SetValue(property, value, null);
                    break;

                case SerializedPropertyType.Enum: // Would be complex because enumValueIndex can't be cast directly.
                case SerializedPropertyType.Generic:
                default:
                    GetAccessor(property)?.SetValue(targetObject, value);
                    break;
            }
        }

        /// <summary>
        /// Sets the value of the <see cref="SerializedProperty"/>.
        /// </summary>
        public static void SetValue(this SerializedProperty property, object value)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean:
                    property.boolValue = (bool)value;
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = (float)value;
                    break;
                case SerializedPropertyType.Integer:
                    property.intValue = (int)value;
                    break;
                case SerializedPropertyType.String:
                    property.stringValue = (string)value;
                    break;

                case SerializedPropertyType.Vector2:
                    property.vector2Value = (Vector2)value;
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = (Vector3)value;
                    break;
                case SerializedPropertyType.Vector4:
                    property.vector4Value = (Vector4)value;
                    break;

                case SerializedPropertyType.Quaternion:
                    property.quaternionValue = (Quaternion)value;
                    break;
                case SerializedPropertyType.Color:
                    property.colorValue = (Color)value;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    property.animationCurveValue = (AnimationCurve)value;
                    break;

                case SerializedPropertyType.Rect:
                    property.rectValue = (Rect)value;
                    break;
                case SerializedPropertyType.Bounds:
                    property.boundsValue = (Bounds)value;
                    break;

                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = (Vector2Int)value;
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = (Vector3Int)value;
                    break;
                case SerializedPropertyType.RectInt:
                    property.rectIntValue = (RectInt)value;
                    break;
                case SerializedPropertyType.BoundsInt:
                    property.boundsIntValue = (BoundsInt)value;
                    break;

                case SerializedPropertyType.ObjectReference:
                    property.objectReferenceValue = (Object)value;
                    break;
                case SerializedPropertyType.ExposedReference:
                    property.exposedReferenceValue = (Object)value;
                    break;

                case SerializedPropertyType.ArraySize:
                    property.intValue = (int)value;
                    break;

                case SerializedPropertyType.FixedBufferSize:
                    throw new InvalidOperationException($"{nameof(SetValue)} failed:" +
                                                        $" {nameof(SerializedProperty)}.{nameof(SerializedProperty.fixedBufferSize)} is read-only.");

                case SerializedPropertyType.Generic:
                case SerializedPropertyType.Enum:
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.Gradient:
                case SerializedPropertyType.Character:
                default:
                    var accessor = GetAccessor(property);
                    if (accessor != null)
                    {
                        var targets = property.serializedObject.targetObjects;
                        foreach (var obj in targets) accessor.SetValue(obj, value);
                    }

                    break;
            }
        }


        public const BindingFlags
            InstanceBindings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private const string
            ArrayDataPrefix = ".Array.data[",
            ArrayDataSuffix = "]";

        private static PropertyInfo _GradientValue;

        /// <summary><c>SerializedProperty.gradientValue</c> is internal.</summary>
        private static PropertyInfo GradientValue
        {
            get
            {
                if (_GradientValue == null)
                    _GradientValue = typeof(SerializedProperty).GetProperty("gradientValue", InstanceBindings);

                return _GradientValue;
            }
        }

        /// <summary>
        /// Gets the value of the specified <see cref="SerializedProperty"/>.
        /// </summary>
        public static object GetValue(this SerializedProperty property, object targetObject)
        {
            if (property.hasMultipleDifferentValues &&
                property.serializedObject.targetObject != targetObject as Object)
            {
                property = new SerializedObject(targetObject as Object).FindProperty(property.propertyPath);
            }

            switch (property.propertyType)
            {
                case SerializedPropertyType.Boolean: return property.boolValue;
                case SerializedPropertyType.Float: return property.floatValue;
                case SerializedPropertyType.String: return property.stringValue;

                case SerializedPropertyType.Integer:
                case SerializedPropertyType.Character:
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.ArraySize:
                    return property.intValue;

                case SerializedPropertyType.Vector2: return property.vector2Value;
                case SerializedPropertyType.Vector3: return property.vector3Value;
                case SerializedPropertyType.Vector4: return property.vector4Value;

                case SerializedPropertyType.Quaternion: return property.quaternionValue;
                case SerializedPropertyType.Color: return property.colorValue;
                case SerializedPropertyType.AnimationCurve: return property.animationCurveValue;

                case SerializedPropertyType.Rect: return property.rectValue;
                case SerializedPropertyType.Bounds: return property.boundsValue;

                case SerializedPropertyType.Vector2Int: return property.vector2IntValue;
                case SerializedPropertyType.Vector3Int: return property.vector3IntValue;
                case SerializedPropertyType.RectInt: return property.rectIntValue;
                case SerializedPropertyType.BoundsInt: return property.boundsIntValue;

                case SerializedPropertyType.ObjectReference: return property.objectReferenceValue;
                case SerializedPropertyType.ExposedReference: return property.exposedReferenceValue;

                case SerializedPropertyType.FixedBufferSize: return property.fixedBufferSize;

                case SerializedPropertyType.Gradient: return GradientValue.GetValue(property, null);

                case SerializedPropertyType.Enum: // Would be complex because enumValueIndex can't be cast directly.
                case SerializedPropertyType.Generic:
                default: return GetAccessor(property)?.GetValue(targetObject);
            }
        }

        /// <summary>
        /// Returns an <see cref="PropertyAccessor"/> that can be used to access the details of the specified `property`.
        /// </summary>
        public static PropertyAccessor GetAccessor(this SerializedProperty property)
        {
            var type = property.serializedObject.targetObject.GetType();
            return GetAccessor(property, property.propertyPath, ref type);
        }

        private static readonly Dictionary<Type, Dictionary<string, PropertyAccessor>>
            TypeToPathToAccessor = new Dictionary<Type, Dictionary<string, PropertyAccessor>>();

        /// <summary>
        /// Returns an <see cref="PropertyAccessor"/> for a <see cref="SerializedProperty"/> with the specified `propertyPath`
        /// on the specified `type` of object.
        /// </summary>
        private static PropertyAccessor GetAccessor(SerializedProperty property, string propertyPath, ref Type type)
        {
            if (!TypeToPathToAccessor.TryGetValue(type, out var pathToAccessor))
            {
                pathToAccessor = new Dictionary<string, PropertyAccessor>();
                TypeToPathToAccessor.Add(type, pathToAccessor);
            }

            if (!pathToAccessor.TryGetValue(propertyPath, out var accessor))
            {
                var nameStartIndex = propertyPath.LastIndexOf('.');
                string elementName;
                PropertyAccessor parent;

                // Array.
                if (nameStartIndex > 6 &&
                    nameStartIndex < propertyPath.Length - 7 &&
                    string.Compare(propertyPath, nameStartIndex - 6, ArrayDataPrefix, 0, 12) == 0)
                {
                    var index = int.Parse(propertyPath.Substring(nameStartIndex + 6, propertyPath.Length - nameStartIndex - 7));

                    var nameEndIndex = nameStartIndex - 6;
                    nameStartIndex = propertyPath.LastIndexOf('.', nameEndIndex - 1);

                    elementName = propertyPath.Substring(nameStartIndex + 1, nameEndIndex - nameStartIndex - 1);

                    FieldInfo field;
                    if (nameStartIndex >= 0)
                    {
                        parent = GetAccessor(property, propertyPath.Substring(0, nameStartIndex), ref type);
                        field = GetField(parent, property, type, elementName);
                    }
                    else
                    {
                        parent = null;
                        field = GetField(type, elementName);
                    }

                    accessor = new CollectionPropertyAccessor(parent, elementName, field, index);
                }
                else // Single.
                {
                    if (nameStartIndex >= 0)
                    {
                        elementName = propertyPath.Substring(nameStartIndex + 1);
                        parent = GetAccessor(property, propertyPath.Substring(0, nameStartIndex), ref type);
                    }
                    else
                    {
                        elementName = propertyPath;
                        parent = null;
                    }

                    var field = GetField(parent, property, type, elementName);

                    accessor = new PropertyAccessor(parent, elementName, field);
                }

                pathToAccessor.Add(propertyPath, accessor);
            }

            if (accessor != null)
            {
                var field = accessor.GetField(property);
                if (field != null)
                {
                    type = field.FieldType;
                }
                else
                {
                    var value = accessor.GetValue(property);
                    type = value?.GetType();
                }
            }

            return accessor;
        }

        /// <summary>Returns a field with the specified `name` in the `declaringType` or any of its base types.</summary>
        /// <remarks>Uses the <see cref="InstanceBindings"/>.</remarks>
        internal static FieldInfo GetField(PropertyAccessor accessor, SerializedProperty property, Type declaringType, string name)
        {
            declaringType = accessor?.GetFieldElementType(property) ?? declaringType;
            return GetField(declaringType, name);
        }

        /// <summary>Returns a field with the specified `name` in the `declaringType` or any of its base types.</summary>
        /// <remarks>Uses the <see cref="InstanceBindings"/>.</remarks>
        internal static FieldInfo GetField(Type declaringType, string name)
        {
            while (declaringType != null)
            {
                var field = declaringType.GetField(name, InstanceBindings);
                if (field != null)
                    return field;

                declaringType = declaringType.BaseType;
            }

            return null;
        }
    }
}