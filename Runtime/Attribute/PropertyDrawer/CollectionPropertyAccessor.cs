#region

using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

#endregion

namespace AIO
{
    /// <summary>[Editor-Only] A <see cref="PropertyAccessor"/> for a specific element index in a collection.</summary>
    public class CollectionPropertyAccessor : PropertyAccessor
    {
        /// <summary>The index of the array element this accessor targets.</summary>
        public readonly int ElementIndex;

        /// <summary>[Internal] Creates a new <see cref="CollectionPropertyAccessor"/>.</summary>
        internal CollectionPropertyAccessor(
            PropertyAccessor parent,
            string           name,
            FieldInfo        field,
            int              elementIndex
        ) : base(parent, name, field, GetElementType(field?.FieldType))
        {
            ElementIndex = elementIndex;
        }

        /// <inheritdoc/>
        public override Type GetFieldElementType(object obj)
        {
            return FieldElementType ?? GetElementType(GetField(ref obj)?.FieldType);
        }

        /// <summary>Returns the type of elements in the array.</summary>
        public static Type GetElementType(Type fieldType)
        {
            if (fieldType == null)
                return null;

            if (fieldType.IsArray)
                return fieldType.GetElementType();

            if (fieldType.IsGenericType)
                return fieldType.GetGenericArguments()[0];

            Debug.LogWarning(
                $"{nameof(AIO)}.{nameof(CollectionPropertyAccessor)}: unable to determine element type for {fieldType}");
            return fieldType;
        }


        /// <summary>Returns the collection object targeted by this accessor.</summary>
        public object GetCollection(object obj)
        {
            return base.GetValue(obj);
        }

        /// <inheritdoc/>
        public override object GetValue(object obj)
        {
            var collection = base.GetValue(obj);
            switch (collection)
            {
                case null: return null;
                case IList list when ElementIndex < list.Count:
                    return list[ElementIndex];
                case IList list:
                    return null;
            }

            var enumerator = ((IEnumerable)collection).GetEnumerator();

            for (var i = 0; i < ElementIndex; i++)
                if (!enumerator.MoveNext())
                    return null;

            return enumerator.Current;
        }


        /// <summary>Sets the collection object targeted by this accessor.</summary>
        public void SetCollection(object obj, object value)
        {
            base.SetValue(obj, value);
        }

        /// <inheritdoc/>
        public override void SetValue(object obj, object value)
        {
            var collection = base.GetValue(obj);
            switch (collection)
            {
                case null:
                    return;
                case IList list:
                {
                    if (ElementIndex < list.Count)
                        list[ElementIndex] = value;
                    return;
                }
                default:
                    throw new InvalidOperationException(
                        $"{nameof(SetValue)} failed: field doesn't implement {nameof(IList)}.");
            }
        }


        /// <summary>
        /// Returns a description of this accessor's path.
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()}[{ElementIndex}]";
        }

#if UNITY_EDITOR
        /// <summary>
        /// Returns the
        /// <see cref="UnityEditor.SerializedProperty.propertyPath"/>
        /// of the array containing the target.
        /// </summary>
#endif
        public string GetCollectionPath() => base.GetPath();

#if UNITY_EDITOR
        /// <summary>
        /// Returns this accessor's
        /// <see cref="UnityEditor.SerializedProperty.propertyPath"/>.
        /// </summary>
#endif
        public override string GetPath() => $"{base.GetPath()}.Array.data[{ElementIndex}]";
    }
}