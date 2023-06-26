using System;
using AIO;
using UnityEngine;

namespace UnityEditor
{
    /// <summary>
    /// The possible states for a function in a <see cref="GenericMenu"/>.
    /// </summary>
    public enum MenuFunctionState
    {
        /// <summary>
        /// 正常显示
        /// </summary>
        Normal,

        /// <summary>
        /// 在它旁边有一个复选标记，表示它已被选中。
        /// </summary>
        Selected,

        /// <summary>
        /// 颜色变灰，无法使用。
        /// </summary>
        Disabled,
    }

    /// <summary>
    /// 菜单扩展
    /// </summary>
    public static partial class GenericMenuExtend
    {
        /// <summary>
        /// 添加菜单项，以便为每个属性的目标对象执行指定的函数。
        /// </summary>
        public static void AddFunction(this GenericMenu menu,
            string label,
            MenuFunctionState state,
            GenericMenu.MenuFunction function)
        {
            if (state != MenuFunctionState.Disabled)
            {
                menu.AddItem(new GUIContent(label), state == MenuFunctionState.Selected, function);
            }
            else
            {
                menu.AddDisabledItem(new GUIContent(label));
            }
        }

        /// <summary>
        /// 添加菜单项，以便为每个属性的目标对象执行指定的函数。
        /// </summary>
        public static void AddFunction(this GenericMenu menu,
            string label,
            bool enabled,
            GenericMenu.MenuFunction function)
            => AddFunction(menu, label, enabled ? MenuFunctionState.Normal : MenuFunctionState.Disabled, function);

        /// <summary>
        /// 添加菜单项，以便为每个属性的目标对象执行指定的函数。
        /// </summary>
        public static void AddPropertyModifierFunction(this GenericMenu menu,
            SerializedProperty property,
            string label,
            bool enabled,
            Action<SerializedProperty> function)
            => AddPropertyModifierFunction(menu, property, label, enabled ? MenuFunctionState.Normal : MenuFunctionState.Disabled, function);

        /// <summary>
        /// 添加一个菜单项，以便为每个属性的目标对象执行指定的函数。
        /// </summary>
        public static void AddPropertyModifierFunction(this GenericMenu menu,
            SerializedProperty property,
            string label,
            Action<SerializedProperty> function)
            => AddPropertyModifierFunction(menu, property, label, MenuFunctionState.Normal, function);

        /// <summary>
        /// 为每个属性的目标对象添加一个菜单项，以执行指定的函数。
        /// </summary>
        public static void AddPropertyModifierFunction(this GenericMenu menu,
            SerializedProperty property,
            string label,
            MenuFunctionState state,
            Action<SerializedProperty> function)
        {
            if (state != MenuFunctionState.Disabled && GUI.enabled)
            {
                menu.AddItem(new GUIContent(label), state == MenuFunctionState.Selected, () =>
                {
                    property.ForEachTarget(function);
                    GUIUtility.keyboardControl = 0;
                    GUIUtility.hotControl = 0;
                    EditorGUIUtility.editingTextField = false;
                });
            }
            else
            {
                menu.AddDisabledItem(new GUIContent(label));
            }
        }
    }
}