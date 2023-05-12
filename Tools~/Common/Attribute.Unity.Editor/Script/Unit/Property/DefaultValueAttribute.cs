﻿using System;
using System.Diagnostics;
using System.Reflection;
using UnityEditor;

namespace AIO
{
    /// <summary>[Editor-Conditional] Specifies the default value of a field and a secondary fallback.</summary>
    /// https://kybernetik.com.au/animancer/api/Animancer/DefaultValueAttribute
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional(Strings.UnityEditor)]
    public class DefaultValueAttribute : Attribute
    {
        /// <summary>The main default value.</summary>
        public object Primary { get; protected set; }


        /// <summary>The fallback value to use if the target value was already equal to the <see cref="Primary"/>.</summary>
        public object Secondary { get; protected set; }

        /// <summary>Creates a new <see cref="DefaultValueAttribute"/>.</summary>
        public DefaultValueAttribute(object primary, object secondary = null)
        {
            Primary = primary;
            Secondary = secondary;
        }


        /// <summary>Creates a new <see cref="DefaultValueAttribute"/>.</summary>
        private DefaultValueAttribute()
        {
        }


        /// <summary>[Editor-Only]
        /// If the field represented by the `property` has a <see cref="DefaultValueAttribute"/>, this method sets
        /// the `value` to its <see cref="Primary"/> value. If it was already at the value, it sets it to the
        /// <see cref="Secondary"/> value instead. And if the field has no attribute, it uses the default for the type.
        /// </summary>
        public static void SetToDefault<T>(ref T value, SerializedProperty property)
        {
            var type = property.serializedObject.targetObject.GetType();
            var accessor = property.GetAccessor();
            var field = accessor.GetField(property);
            if (field == null)
                accessor.SetValue(property, null);
            else
                SetToDefault(ref value, field);
        }

        /// <summary>[Editor-Only]
        /// If the field represented by the `property` has a <see cref="DefaultValueAttribute"/>, this method sets
        /// the `value` to its <see cref="Primary"/> value. If it was already at the value, it sets it to the
        /// <see cref="Secondary"/> value instead. And if the field has no attribute, it uses the default for the type.
        /// </summary>
        public static void SetToDefault<T>(ref T value, FieldInfo field)
        {
            var attributeType = typeof(DefaultValueAttribute);
            var defaults = field.IsDefined(attributeType, false)
                ? (DefaultValueAttribute)field.GetCustomAttributes(attributeType, false)[0]
                : default;

            if (defaults != null) defaults.SetToDefault(ref value);
            else value = default;
        }


        /// <summary>[Editor-Only]
        /// Sets the `value` equal to the <see cref="Primary"/> value. If it was already at the value, it sets it equal
        /// to the <see cref="Secondary"/> value instead.
        /// </summary>
        public void SetToDefault<T>(ref T value)
        {
            var primary = Primary;
            if (!Equals(value, primary))
            {
                value = (T)primary;
                return;
            }

            var secondary = Secondary;
            if (secondary != null || !typeof(T).IsValueType)
            {
                value = (T)secondary;
                return;
            }
        }


        /// <summary>[Editor-Only]
        /// Sets the `value` equal to the `primary` value. If it was already at the value, it sets it equal to the
        /// `secondary` value instead.
        /// </summary>
        public static void SetToDefault<T>(ref T value, T primary, T secondary)
        {
            if (!Equals(value, primary))
                value = primary;
            else
                value = secondary;
        }
    }
}