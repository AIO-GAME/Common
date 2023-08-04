/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        #region 屏蔽文本框 FieldMask

        /// <summary> 屏蔽文本框 FieldMask </summary>
        /// <param name="mask">选择值</param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static int Mask(int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions, style, options);
        }

        /// <summary> 屏蔽文本框 FieldMask </summary>
        /// <param name="mask">选择值</param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Mask(string label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, options);
        }

        /// <summary> 屏蔽文本框 FieldMask </summary>
        /// <param name="mask">选择值</param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Mask(string label, int mask, IEnumerable<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions.ToArray(), options);
        }

        /// <summary> 屏蔽文本框 FieldMask </summary>
        /// <param name="mask">选择值</param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Mask(GUIContent label, int mask, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, options);
        }

        /// <summary> 屏蔽文本框 FieldMask </summary>
        /// <param name="mask">选择值</param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Mask(string label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, style, options);
        }

        /// <summary> 屏蔽文本框 FieldMask </summary>
        /// <param name="mask">选择值</param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static int Mask(GUIContent label, int mask, string[] displayedOptions, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(label, mask, displayedOptions, style, options);
        }

        /// <summary> 屏蔽文本框 FieldMask </summary>
        /// <param name="mask">选择值</param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式</param>
        public static int Mask(int mask, string[] displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions, options);
        }

        /// <summary> 屏蔽文本框 FieldMask </summary>
        /// <param name="mask">选择值</param>
        /// <param name="displayedOptions">选择内容</param>
        /// <param name="options">排版格式</param>
        public static int Mask(int mask, ICollection<string> displayedOptions, params GUILayoutOption[] options)
        {
            return EditorGUILayout.MaskField(mask, displayedOptions.ToArray(), options);
        }

        #endregion
    }
}
