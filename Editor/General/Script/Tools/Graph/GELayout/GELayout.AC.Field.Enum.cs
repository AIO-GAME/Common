// /*|✩ - - - - - |||
// |||✩ Author:   ||| -> XINAN
// |||✩ Date:     ||| -> 2023-06-29
// |||✩ Document: ||| ->
// |||✩ - - - - - |*/
//
// using System;
// using UnityEditor;
// using UnityEngine;
//
// namespace AIO.UEditor
// {
//     public partial class GELayout
//     {
//         #region 枚举菜单 EnumMaskField 已弃用
//
// #if !UNITY_2020_1_OR_NEWER
//             public static Enum FieldEnumMask(string label, Enum enumValue, GUIStyle style, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskField(label, enumValue, style, options);
//
//             public static Enum FieldEnumMask(GUIContent label, Enum enumValue, GUIStyle style, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskField(label, enumValue, style, options);
//
//             public static Enum FieldEnumMask(Enum enumValue, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskField(enumValue, options);
//
//             public static Enum FieldEnumMask(Enum enumValue, GUIStyle style, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskField(enumValue, style, options);
//
//             public static Enum FieldEnumMask(string label, Enum enumValue, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskField(label, enumValue, options);
//
//             public static Enum FieldEnumMask(GUIContent label, Enum enumValue, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskField(label, enumValue, options);
// #endif
//
//         #endregion
//
//         #region 枚举菜单 EnumMaskPopup 已弃用
//
// #if !UNITY_2020_1_OR_NEWER
//             public static Enum PopupEnumMask(string label, Enum selected, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskPopup(label, selected, options);
//
//             public static Enum PopupEnumMask(GUIContent label, Enum selected, GUIStyle style, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskPopup(label, selected, style, options);
//
//             public static Enum PopupEnumMask(string label, Enum selected, GUIStyle style, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskPopup(label, selected, style, options);
//
//             public static Enum PopupEnumMask(GUIContent label, Enum selected, params GUILayoutOption[] options)
//                 => EditorGUILayout.EnumMaskPopup(label, selected, options);
// #endif
//
//         #endregion
//
//         #region 枚举菜单 EnumFlagsField
//
// #if UNITY_2018_1_OR_NEWER
//
//         /// <summary> 枚举菜单 EnumFlagsField </summary>
//         /// <param name="enumValue">枚举值</param>
//         /// <param name="options">排版格式</param>
//         public static T FieldEnumFlags<T>(T enumValue, params GUILayoutOption[] options) where T : Enum
//         {
//             return (T)EditorGUILayout.EnumFlagsField(enumValue, options);
//         }
//
//         /// <summary> 枚举菜单 EnumFlagsField </summary>
//         /// <param name="label">标签</param>
//         /// <param name="enumValue">枚举值</param>
//         /// <param name="includeObsolete">true:包含带有eteattribute的枚举值,false:排除</param>
//         /// <param name="style">显示风格</param>
//         /// <param name="options">排版格式</param>
//         public static T FieldEnumFlags<T>(GUIContent label, T enumValue, bool includeObsolete, GUIStyle style, params GUILayoutOption[] options) where T : Enum
//         {
//             return (T)EditorGUILayout.EnumFlagsField(label, enumValue, includeObsolete, style, options);
//         }
//
//         /// <summary> 枚举菜单 EnumFlagsField </summary>
//         /// <param name="label">标签</param>
//         /// <param name="enumValue">枚举值</param>
//         /// <param name="includeObsolete">true:包含带有eteattribute的枚举值,false:排除</param>
//         /// <param name="options">排版格式</param>
//         public static T FieldEnumFlags<T>(GUIContent label, T enumValue, bool includeObsolete, params GUILayoutOption[] options) where T : Enum
//         {
//             return (T)EditorGUILayout.EnumFlagsField(label, enumValue, includeObsolete, options);
//         }
//
//         /// <summary> 枚举菜单 EnumFlagsField </summary>
//         /// <param name="label">标签</param>
//         /// <param name="enumValue">枚举值</param>
//         /// <param name="style">显示风格</param>
//         /// <param name="options">排版格式</param>
//         public static T FieldEnumFlags<T>(GUIContent label, T enumValue, GUIStyle style, params GUILayoutOption[] options) where T : Enum
//         {
//             return (T)EditorGUILayout.EnumFlagsField(label, enumValue, style, options);
//         }
//
//         /// <summary> 枚举菜单 EnumFlagsField </summary>
//         /// <param name="label">标签</param>
//         /// <param name="enumValue">枚举值</param>
//         /// <param name="options">排版格式</param>
//         public static T FieldEnumFlags<T>(GUIContent label, T enumValue, params GUILayoutOption[] options) where T : Enum
//         {
//             return (T)EditorGUILayout.EnumFlagsField(label, enumValue, options);
//         }
//
//         /// <summary> 枚举菜单 EnumFlagsField </summary>
//         /// <param name="label">标签</param>
//         /// <param name="enumValue">枚举值</param>
//         /// <param name="style">显示风格</param>
//         /// <param name="options">排版格式</param>
//         public static T FieldEnumFlags<T>(string label, T enumValue, GUIStyle style, params GUILayoutOption[] options) where T : Enum
//         {
//             return (T)EditorGUILayout.EnumFlagsField(label, enumValue, style, options);
//         }
//
//         /// <summary> 枚举菜单 EnumFlagsField </summary>
//         /// <param name="label">标签</param>
//         /// <param name="enumValue">枚举值</param>
//         /// <param name="options">排版格式</param>
//         public static T FieldEnumFlags<T>(string label, T enumValue, params GUILayoutOption[] options) where T : Enum
//         {
//             return (T)EditorGUILayout.EnumFlagsField(label, enumValue, options);
//         }
//
//         /// <summary> 枚举菜单 EnumFlagsField </summary>
//         /// <param name="enumValue">枚举值</param>
//         /// <param name="style">显示风格</param>
//         /// <param name="options">排版格式</param>
//         public static T FieldEnumFlags<T>(T enumValue, GUIStyle style, params GUILayoutOption[] options) where T : Enum
//         {
//             return (T)EditorGUILayout.EnumFlagsField(enumValue, style, options);
//         }
// #endif
//
//         #endregion
//     }
// }
