/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Internal;
using Object = UnityEngine.Object;

namespace UnityEditor
{
    public partial class GELayout
    {
        #region 弹出整数选择字段 Popup Int

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="label">标签</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="options">排版格式</param>
        public static int Popup(GUIContent label, int selectedValue, IEnumerable<GUIContent> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions.ToArray(), optionValues, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="label">标签</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="options">排版格式</param>
        public static int Popup(string label, int selectedValue, string[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions, optionValues, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="label">标签</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="options">排版格式</param>
        public static int Popup(string label, int selectedValue, IEnumerable<string> displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions.ToArray(), optionValues, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="label">标签</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="options">排版格式</param>
        public static int Popup(string label, int selectedValue, int[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            var vs = new string[displayedOptions.Length];
            var index = 0;
            foreach (var item in displayedOptions)
                vs[index++] = item.ToString();
            return EditorGUILayout.IntPopup(label, selectedValue, vs, optionValues, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Popup(int selectedValue, GUIContent[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(selectedValue, displayedOptions, optionValues, style, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="label">标签</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Popup(GUIContent label, int selectedValue, GUIContent[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions, optionValues, style, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="label">标签</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static int Popup(string label, int selectedValue, string[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(label, selectedValue, displayedOptions, optionValues, style, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="options">排版格式</param>
        public static int Popup(int selectedValue, string[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(selectedValue, displayedOptions, optionValues, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static int Popup(int selectedValue, string[] displayedOptions, int[] optionValues, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(selectedValue, displayedOptions, optionValues, style, options);
        }

        /// <summary> 弹出整数选择字段 </summary>
        /// <param name="displayedOptions">一个具有用户可以选择的显示选项的数组</param>
        /// <param name="optionValues">包含每个选项的值的数组</param>
        /// <param name="selectedValue">字段显示的选项的值</param>
        /// <param name="options">排版格式</param>
        public static int Popup(int selectedValue, GUIContent[] displayedOptions, int[] optionValues, params GUILayoutOption[] options)
        {
            return EditorGUILayout.IntPopup(selectedValue, displayedOptions, optionValues, options);
        }

        #endregion

        #region 弹窗 Popup

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Popup(int selectedIndex, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, style, options);
        }

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Popup(string label, int selectedIndex, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, style, options);
        }

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Popup(GUIContent label, int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);
        }

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Popup(string label, int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);
        }

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Popup(GUIContent label, int selectedIndex, GUIContent[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, options);
        }

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Popup(GUIContent label, int selectedIndex, GUIContent[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, style, options);
        }

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式</param>
        public static int Popup(int selectedIndex, GUIContent[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, options);
        }

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Popup(int selectedIndex, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, style, options);
        }

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式</param>
        public static int Popup(int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions, options);
        }

        #endregion

        #region 弹窗枚举菜单 Popup Enum

#if UNITY_2018_3_OR_NEWER

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="selected">枚举值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(selected, style, options);
        }

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="label">标签</param>
        /// <param name="selected">枚举值</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(string label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, options);
        }

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="label">标签</param>
        /// <param name="selected">枚举值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(string label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, style, options);
        }

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="label">标签</param>
        /// <param name="selected">枚举值</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(GUIContent label, T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, options);
        }

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="label">标签</param>
        /// <param name="selected">枚举值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(GUIContent label, T selected, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, style, options);
        }

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="label">标签</param>
        /// <param name="selected">枚举值</param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法</param>
        /// <param name="includeObsolete">true:包含带有eteattribute的枚举值,false:排除</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, options);
        }

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="label">标签</param>
        /// <param name="selected">枚举值</param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法</param>
        /// <param name="includeObsolete">true:包含带有eteattribute的枚举值,false:排除</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, style, options);
        }

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="selected">枚举值</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(T selected, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(selected, options);
        }
#endif

        #endregion
    }
}