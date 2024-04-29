#if UNITY_EDITOR

#region

using System;
using System.Reflection;
using UnityEditor;

#endregion

namespace AIO
{
    /// <summary>[Editor-Only]
    /// A wrapper for accessing the underlying values and fields of a <see cref="SerializedProperty"/>.
    /// </summary>
    public partial class PropertyAccessor
    {
        /// <summary>
        /// Calls <see cref="GetField(object)"/> with the <see cref="SerializedObject.targetObject"/>.
        /// </summary>
        public FieldInfo GetField(SerializedObject serializedObject)
        {
            return serializedObject != null ? GetField(serializedObject.targetObject) : null;
        }

        /// <summary>
        /// Calls <see cref="GetField(SerializedObject)"/> with the
        /// <see cref="SerializedProperty.serializedObject"/>.
        /// </summary>
        public FieldInfo GetField(SerializedProperty serializedProperty)
        {
            return serializedProperty != null ? GetField(serializedProperty.serializedObject) : null;
        }


        /// <summary>
        /// Calls <see cref="GetFieldElementType(object)"/> with the
        /// <see cref="SerializedObject.targetObject"/>.
        /// </summary>
        public Type GetFieldElementType(SerializedObject serializedObject)
        {
            return serializedObject != null ? GetFieldElementType(serializedObject.targetObject) : null;
        }

        /// <summary>
        /// Calls <see cref="GetFieldElementType(SerializedObject)"/> with the
        /// <see cref="SerializedProperty.serializedObject"/>.
        /// </summary>
        public Type GetFieldElementType(SerializedProperty serializedProperty)
        {
            return serializedProperty != null ? GetFieldElementType(serializedProperty.serializedObject) : null;
        }

        /// <summary>
        /// Gets the value of the from the <see cref="Parent"/> (if there is one), then uses it to get and return
        /// the value of the <see cref="Field"/>.
        /// </summary>
        public object GetValue(SerializedObject serializedObject)
        {
            return serializedObject != null ? GetValue(serializedObject.targetObject) : null;
        }

        /// <summary>
        /// Gets the value of the from the <see cref="Parent"/> (if there is one), then uses it to get and return
        /// the value of the <see cref="Field"/>.
        /// </summary>
        public object GetValue(SerializedProperty serializedProperty)
        {
            return serializedProperty != null ? GetValue(serializedProperty.serializedObject.targetObject) : null;
        }

        /// <summary>
        /// Gets the value of the from the <see cref="Parent"/> (if there is one), then uses it to set the value
        /// of the <see cref="Field"/>.
        /// </summary>
        public void SetValue(SerializedObject serializedObject, object value)
        {
            if (serializedObject != null)
                SetValue(serializedObject.targetObject, value);
        }

        /// <summary>
        /// Gets the value of the from the <see cref="Parent"/> (if there is one), then uses it to set the value
        /// of the <see cref="Field"/>.
        /// </summary>
        public void SetValue(SerializedProperty serializedProperty, object value)
        {
            if (serializedProperty != null)
                SetValue(serializedProperty.serializedObject, value);
        }

        /// <summary>
        /// Resets the value of the <see cref="SerializedProperty"/> to the default value of its type by executing
        /// its constructor and field initializers.
        /// </summary>
        /// <remarks>
        /// If you don't want to run constructors and field initializers, you can call
        /// </remarks>
        /// <example><code>
        /// SerializedProperty property;
        /// property.GetAccessor().ResetValue(property);
        /// </code></example>
        public void ResetValue(SerializedProperty property, string undoName = "Inspector")
        {
            Undo.RecordObjects(property.serializedObject.targetObjects, undoName);
            property.serializedObject.ApplyModifiedProperties();

            var type = GetValue(property)?.GetType();
            var value = type != null ? Activator.CreateInstance(type) : null;
            SetValue(property, value);

            property.serializedObject.Update();
        }
    }
}

#endif