﻿using System.Diagnostics;

using UnityEditor;

using UnityEngine;

namespace AIO
{
    /// <summary>[Editor-Conditional]
    /// 一个PropertyAttribute，它可以自己绘制，而不需要单独的PropertyDrawer
    /// </summary>
    [Conditional(Strings.UnityEditor)]
    public abstract class SelfDrawerAttribute : PropertyAttribute
    {
        /// <summary>[Editor-Only]
        /// Can the GUI for the `property` be cached?
        /// </summary>
        public virtual bool CanCacheInspectorGUI(SerializedProperty property) => true;

        /// <summary>[Editor-Only]
        /// Calculates the height of the GUI for the `property`.
        /// </summary>
        public virtual float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUIUtility.singleLineHeight;

        /// <summary>[Editor-Only]
        /// Draws the GUI for the `property`.
        /// </summary>
        public abstract void OnGUI(Rect area, SerializedProperty property, GUIContent label);
    }
}