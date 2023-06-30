/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

namespace UnityEngine
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
        /// <param name="text">值</param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, GUIStyle text, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(label, text, options);
        }

        /// <summary> 文本文本框 FieldText </summary>
        /// <param name="label">标签</param>
        /// <param name="text">值</param>
        /// <param name="options">排版格式</param>
        public static string Field(string label, string text, params GUILayoutOption[] options)
        {
            return GUILayout.TextField(label, text, options);
        }

        #endregion
    }
}