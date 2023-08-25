/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEngine;

namespace AIO
{
    public partial class GULayout
    {
        #region 文本文本框 FieldText

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="text">值</param>
        /// <param name="options">排版格式</param>
        public static string Field(string text, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(text, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="text">值</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, int text, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(label, text, style, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(label, style, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, string style, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(label, style, options);
        }

        #endregion


        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static int Field(string label, int value, params GUILayoutOption[] options)
        {
            return int.Parse(GUILayout.TextField(label, value, options));
        }
    }
}
