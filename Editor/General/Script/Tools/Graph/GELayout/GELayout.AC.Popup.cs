/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        #region 弹出整数选择字段 Popup Int

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
        public static int Popup(string label, int selectedIndex, ICollection<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions.ToArray(), options);
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

        /// <summary> 弹窗 Popup </summary>
        /// <param name="selectedIndex">值</param>
        /// <param name="displayedOptions">弹窗内容</param>
        /// <param name="options">排版格式</param>
        public static int Popup(int selectedIndex, ICollection<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Popup(selectedIndex, displayedOptions.ToArray(), options);
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
        /// <param name="includeObsolete">true:包含带有attribute的枚举值,false:排除</param>
        /// <param name="options">排版格式</param>
        public static T Popup<T>(GUIContent label, T selected, Func<Enum, bool> checkEnabled, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
        {
            return (T)EditorGUILayout.EnumPopup(label, selected, checkEnabled, includeObsolete, options);
        }

        /// <summary> 弹窗枚举菜单 </summary>
        /// <param name="label">标签</param>
        /// <param name="selected">枚举值</param>
        /// <param name="checkEnabled">显示每个Enum值,返回指定的方法</param>
        /// <param name="includeObsolete">true:包含带有attribute的枚举值,false:排除</param>
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
