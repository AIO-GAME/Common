namespace AIO
{
    using System;
    using System.Reflection;
    using UnityEditor;

    /// <summary>[Editor-Only]
    /// A wrapper for accessing the underlying values and fields of a <see cref="SerializedProperty"/>.
    /// </summary>
    public class PropertyAccessor
    {
        /************************************************************************************************************************/

        /// <summary>The accessor for the field which this accessor is nested inside.</summary>
        public readonly PropertyAccessor Parent;

        /// <summary>The name of the field wrapped by this accessor.</summary>
        public readonly string Name;

        /// <summary>The field wrapped by this accessor.</summary>
        protected readonly FieldInfo Field;

        /// <summary>
        /// The type of the wrapped <see cref="Field"/>.
        /// Or if it's a collection, this is the type of items in the collection.
        /// </summary>
        protected readonly Type FieldElementType;

        /************************************************************************************************************************/

        /// <summary>[Internal] Creates a new <see cref="PropertyAccessor"/>.</summary>
        internal PropertyAccessor(PropertyAccessor parent, string name, FieldInfo field)
            : this(parent, name, field, field?.FieldType)
        {
        }

        /// <summary>Creates a new <see cref="PropertyAccessor"/>.</summary>
        protected PropertyAccessor(PropertyAccessor parent, string name, FieldInfo field, Type fieldElementType)
        {
            Parent = parent;
            Name = name;
            Field = field;
            FieldElementType = fieldElementType;
        }

        /************************************************************************************************************************/

        /// <summary>Returns the <see cref="Field"/> if there is one or tries to get it from the object's type.</summary>
        /// 
        /// <remarks>
        /// If this accessor has a <see cref="Parent"/>, the `obj` must be associated with the root
        /// <see cref="SerializedProperty"/> and this method will change it to reference the parent field's value.
        /// </remarks>
        /// 
        /// <example><code>
        /// [Serializable]
        /// public class InnerClass
        /// {
        ///     public float value;
        /// }
        /// 
        /// [Serializable]
        /// public class RootClass
        /// {
        ///     public InnerClass inner;
        /// }
        /// 
        /// public class MyBehaviour : MonoBehaviour
        /// {
        ///     public RootClass root;
        /// }
        /// 
        /// [UnityEditor.CustomEditor(typeof(MyBehaviour))]
        /// public class MyEditor : UnityEditor.Editor
        /// {
        ///     private void OnEnable()
        ///     {
        ///         var serializedObject = new SerializedObject(target);
        ///         var rootProperty = serializedObject.FindProperty("root");
        ///         var innerProperty = rootProperty.FindPropertyRelative("inner");
        ///         var valueProperty = innerProperty.FindPropertyRelative("value");
        /// 
        ///         var accessor = valueProperty.GetAccessor();
        /// 
        ///         object obj = target;
        ///         var valueField = accessor.GetField(ref obj);
        ///         // valueField is a FieldInfo referring to InnerClass.value.
        ///         // obj now holds the ((MyBehaviour)target).root.inner.
        ///     }
        /// }
        /// </code></example>
        /// 
        public FieldInfo GetField(ref object obj)
        {
            if (Parent != null) obj = Parent.GetValue(obj);
            if (Field != null) return Field;
            if (obj is null) return null;

            var declaringType = obj.GetType();
            while (declaringType != null)
            {
                var field = declaringType.GetField(Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null) return field;
                declaringType = declaringType.BaseType;
            }

            return null;
        }

        /// <summary>Returns a field with the specified `name` in the `declaringType` or any of its base types.</summary>
        public static FieldInfo GetField(Type declaringType, string name)
        {
            while (declaringType != null)
            {
                var field = declaringType.GetField(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                    return field;

                declaringType = declaringType.BaseType;
            }

            return null;
        }

        /// <summary>
        /// Returns the <see cref="Field"/> if there is one, otherwise calls <see cref="GetField(ref object)"/>.
        /// </summary>
        public FieldInfo GetField(object obj)
            => Field ?? GetField(ref obj);

        /// <summary>
        /// Calls <see cref="GetField(object)"/> with the <see cref="SerializedObject.targetObject"/>.
        /// </summary>
        public FieldInfo GetField(SerializedObject serializedObject)
            => serializedObject != null ? GetField(serializedObject.targetObject) : null;

        /// <summary>
        /// Calls <see cref="GetField(SerializedObject)"/> with the
        /// <see cref="SerializedProperty.serializedObject"/>.
        /// </summary>
        public FieldInfo GetField(SerializedProperty serializedProperty)
            => serializedProperty != null ? GetField(serializedProperty.serializedObject) : null;

        /************************************************************************************************************************/

        /// <summary>
        /// Returns the <see cref="FieldElementType"/> if there is one, otherwise calls <see cref="GetField(ref object)"/>
        /// and returns its <see cref="FieldInfo.FieldType"/>.
        /// </summary>
        public virtual Type GetFieldElementType(object obj)
            => FieldElementType ?? GetField(ref obj)?.FieldType;

        /// <summary>
        /// Calls <see cref="GetFieldElementType(object)"/> with the
        /// <see cref="SerializedObject.targetObject"/>.
        /// </summary>
        public Type GetFieldElementType(SerializedObject serializedObject)
            => serializedObject != null ? GetFieldElementType(serializedObject.targetObject) : null;

        /// <summary>
        /// Calls <see cref="GetFieldElementType(SerializedObject)"/> with the
        /// <see cref="SerializedProperty.serializedObject"/>.
        /// </summary>
        public Type GetFieldElementType(SerializedProperty serializedProperty)
            => serializedProperty != null ? GetFieldElementType(serializedProperty.serializedObject) : null;

        /// <summary>
        /// Gets the value of the from the <see cref="Parent"/> (if there is one), then uses it to get and return
        /// the value of the <see cref="Field"/>.
        /// </summary>
        public virtual object GetValue(object obj)
        {
            var field = GetField(ref obj);
            if (field is null ||
                (obj is null && !field.IsStatic))
                return null;

            return field.GetValue(obj);
        }

        /// <summary>
        /// Gets the value of the from the <see cref="Parent"/> (if there is one), then uses it to get and return
        /// the value of the <see cref="Field"/>.
        /// </summary>
        public object GetValue(SerializedObject serializedObject)
            => serializedObject != null ? GetValue(serializedObject.targetObject) : null;

        /// <summary>
        /// Gets the value of the from the <see cref="Parent"/> (if there is one), then uses it to get and return
        /// the value of the <see cref="Field"/>.
        /// </summary>
        public object GetValue(SerializedProperty serializedProperty)
            => serializedProperty != null ? GetValue(serializedProperty.serializedObject.targetObject) : null;

        /// <summary>
        /// Gets the value of the from the <see cref="Parent"/> (if there is one), then uses it to set the value
        /// of the <see cref="Field"/>.
        /// </summary>
        public virtual void SetValue(object obj, object value)
        {
            var field = GetField(ref obj);

            if (field is null ||
                obj is null)
                return;

            field.SetValue(obj, value);
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

        /************************************************************************************************************************/

        /// <summary>Returns a description of this accessor's path.</summary>
        public override string ToString()
        {
            if (Parent != null)
                return $"{Parent}.{Name}";
            else
                return Name;
        }

        /************************************************************************************************************************/

        /// <summary>Returns a this accessor's <see cref="SerializedProperty.propertyPath"/>.</summary>
        public virtual string GetPath()
        {
            if (Parent != null)
                return $"{Parent.GetPath()}.{Name}";
            else
                return Name;
        }

        /************************************************************************************************************************/
    }
}