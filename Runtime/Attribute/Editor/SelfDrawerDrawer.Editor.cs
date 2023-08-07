﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace AIO
{
    [CustomPropertyDrawer(typeof(SelfDrawerAttribute), true)]
    internal sealed partial class SelfDrawerDrawer : PropertyDrawer
    {
        /// <summary>Calls <see cref="SelfDrawerAttribute.CanCacheInspectorGUI"/>.</summary>
        public override bool CanCacheInspectorGUI(SerializedProperty property)
            => Attribute.CanCacheInspectorGUI(property);

        /// <summary>Calls <see cref="SelfDrawerAttribute.GetPropertyHeight"/>.</summary>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => Attribute.GetPropertyHeight(property, label);

        /// <summary>Calls <see cref="SelfDrawerAttribute.OnGUI"/>.</summary>
        public override void OnGUI(Rect area, SerializedProperty property, GUIContent label)
            => Attribute.OnGUI(area, property, label);
    }
}
#endif