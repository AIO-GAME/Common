using System;
using System.Collections;
using System.Reflection;
using UnityEditor;

namespace AIO.UEditor
{
    /// <example>https://gist.github.com/yasirkula/9a00f988cdc7354eef52d46d8db2fe3b</example>
    /// <example>http://answers.unity.com/answers/425602/view.html</example>
    public partial class SerializedPropertyExtend
    {
        /// <summary>
        /// 判断`property`的值是否是该类型的默认值
        /// </summary>
        /// <param name="property"> 序列化属性 </param>
        /// <returns> 是否是默认值 </returns>
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

        /// <summary>
        /// 获取`property`的值
        /// </summary>
        /// <param name="property"> 序列化属性 </param>
        /// <returns> 值 </returns>
        public static object GetRawValue(this SerializedProperty property)
        {
            object   result = property.serializedObject.targetObject;
            string[] path   = property.propertyPath.Replace(".Array.data[", "[").Split('.');
            for (int i = 0; i < path.Length; i++)
            {
                string pathElement = path[i];

                int arrayStartIndex = pathElement.IndexOf('[');
                if (arrayStartIndex < 0)
                    result = GetFieldValue(result, pathElement);
                else
                {
                    string variableName = pathElement.Substring(0, arrayStartIndex);

                    int arrayEndIndex     = pathElement.IndexOf(']', arrayStartIndex + 1);
                    int arrayElementIndex = int.Parse(pathElement.Substring(arrayStartIndex + 1, arrayEndIndex - arrayStartIndex - 1));
                    result = GetFieldValue(result, variableName, arrayElementIndex);
                }
            }

            return result;
        }

        private static object GetFieldValue(object source, string fieldName)
        {
            if (source == null)
                return null;

            FieldInfo fieldInfo = null;
            Type      type      = source.GetType();
            while (fieldInfo == null && type != typeof(object))
            {
                fieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                type      = type.BaseType;
            }

            if (fieldInfo != null)
                return fieldInfo.GetValue(source);

            PropertyInfo propertyInfo = null;
            type = source.GetType();
            while (propertyInfo == null && type != typeof(object))
            {
                propertyInfo = type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);
                type         = type.BaseType;
            }

            if (propertyInfo != null)
                return propertyInfo.GetValue(source, null);

            if (fieldName.Length > 2 && fieldName.StartsWith("m_", StringComparison.OrdinalIgnoreCase))
                return GetFieldValue(source, fieldName.Substring(2));

            return null;
        }

        private static object GetFieldValue(object source, string fieldName, int arrayIndex)
        {
            IEnumerable enumerable = GetFieldValue(source, fieldName) as IEnumerable;
            if (enumerable == null)
                return null;

            if (enumerable is IList)
                return ((IList)enumerable)[arrayIndex];

            IEnumerator enumerator = enumerable.GetEnumerator();
            for (int i = 0; i <= arrayIndex; i++)
                enumerator.MoveNext();

            return enumerator.Current;
        }

        /// <summary>
        /// 设置`property`的值为`value`
        /// 注意：如果`property`的类型是struct，那么`value`的类型必须和`property`的类型完全一致，否则会报错
        /// </summary>
        /// <param name="property"> 序列化属性 </param>
        /// <param name="value"> 值 </param>
        public static void SetRawValue(this SerializedProperty property, object value)
        {
            // Assume we have component A which has a struct variable called B and we want to change B's C variable's value
            // with this function. If all we do is get B's corresponding FieldInfo for C and call its SetValue function, we
            // won't really change the value of A.B.C because B is a struct which was boxed when we called SetValue and we
            // essentially changed a copy of B, not B itself. So, we need to keep a reference to our boxed B variable, change
            // its C variable and then assign the boxed B value back to A. This way, we will in fact change the value of A.B.C
            //
            // In this code, there are 2 for loops. In the first loop, we are basically storing the boxed values (B) in setValues
            // and at the end of the loop, we change B.C's value. In the second loop, we assign boxed values back to their parent
            // variables (assigning boxed B value back to A)
            string[] path      = property.propertyPath.Replace(".Array.data[", "[").Split('.');
            object[] setValues = new object[path.Length];
            setValues[0] = property.serializedObject.targetObject;
            for (int i = 0; i < path.Length; i++)
            {
                string pathElement = path[i];

                int arrayStartIndex = pathElement.IndexOf('[');
                if (arrayStartIndex < 0)
                {
                    if (i < path.Length - 1)
                        setValues[i + 1] = GetFieldValue(setValues[i], pathElement);
                    else
                        SetFieldValue(setValues[i], pathElement, value);
                }
                else
                {
                    string variableName = pathElement.Substring(0, arrayStartIndex);

                    int arrayEndIndex     = pathElement.IndexOf(']', arrayStartIndex + 1);
                    int arrayElementIndex = int.Parse(pathElement.Substring(arrayStartIndex + 1, arrayEndIndex - arrayStartIndex - 1));
                    if (i < path.Length - 1)
                        setValues[i + 1] = GetFieldValue(setValues[i], pathElement, arrayElementIndex);
                    else
                        SetFieldValue(setValues[i], variableName, arrayElementIndex, value);
                }
            }

            for (int i = path.Length - 2; i >= 0; i--)
            {
                string pathElement = path[i];

                int arrayStartIndex = pathElement.IndexOf('[');
                if (arrayStartIndex < 0)
                    SetFieldValue(setValues[i], pathElement, setValues[i + 1]);
                else
                {
                    string variableName = pathElement.Substring(0, arrayStartIndex);

                    int arrayEndIndex     = pathElement.IndexOf(']', arrayStartIndex + 1);
                    int arrayElementIndex = int.Parse(pathElement.Substring(arrayStartIndex + 1, arrayEndIndex - arrayStartIndex - 1));
                    SetFieldValue(setValues[i], variableName, arrayElementIndex, setValues[i + 1]);
                }
            }
        }

        private static void SetFieldValue(object source, string fieldName, object value)
        {
            if (source == null)
                return;

            FieldInfo fieldInfo = null;
            Type      type      = source.GetType();
            while (fieldInfo == null && type != typeof(object))
            {
                fieldInfo = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                type      = type.BaseType;
            }

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(source, value);
                return;
            }

            PropertyInfo propertyInfo = null;
            type = source.GetType();
            while (propertyInfo == null && type != typeof(object))
            {
                propertyInfo = type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.IgnoreCase);
                type         = type.BaseType;
            }

            if (propertyInfo != null)
            {
                propertyInfo.SetValue(source, value, null);
                return;
            }

            if (fieldName.Length > 2 && fieldName.StartsWith("m_", StringComparison.OrdinalIgnoreCase))
                SetFieldValue(source, fieldName.Substring(2), value);
        }

        private static void SetFieldValue(object source, string fieldName, int arrayIndex, object value)
        {
            IEnumerable enumerable = GetFieldValue(source, fieldName) as IEnumerable;
            if (enumerable is IList)
                ((IList)enumerable)[arrayIndex] = value;
        }
    }
}