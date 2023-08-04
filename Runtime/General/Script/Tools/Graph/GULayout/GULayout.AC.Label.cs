/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEngine;

namespace AIO
{
    public partial class GULayout
    {
        #region Label

        /// <summary> 标签 </summary>
        /// <param name="image">图标</param>
        /// <param name="options">排版格式</param>
        public static void Label(Texture image, params GUILayoutOption[] options)
        {
            GUILayout.Label(image, options);
        }

        /// <summary> 标签 </summary>
        /// <param name="obj">内容</param>
        /// <param name="options">排版格式</param>
        public static void Label(object obj, params GUILayoutOption[] options)
        {
            GUILayout.Label(obj.ToString(), options);
        }

        /// <summary> 标签 </summary>
        /// <param name="text">文本内容</param>
        /// <param name="options">排版格式</param>
        public static void Label(string text, params GUILayoutOption[] options)
        {
            GUILayout.Label(text, options);
        }

        /// <summary> 标签 </summary>
        /// <param name="text">文本内容</param>
        /// <param name="options">排版格式</param>
        public static void Label(GUIContent text, params GUILayoutOption[] options)
        {
            GUILayout.Label(text, options);
        }


        /// <summary> 标签 </summary>
        /// <param name="image">图标</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void Label(Texture image, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.Label(image, style, options);
        }

        /// <summary> 标签 </summary>
        /// <param name="text">文本内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.Label(text, style, options);
        }

        /// <summary> 标签 </summary>
        /// <param name="obj">内容</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static void Label(object obj, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.Label(obj.ToString(), style, options);
        }

        /// <summary> 标签 </summary>
        /// <param name="text">文本内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static void Label(GUIContent text, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.Label(text, style, options);
        }

        #endregion
    }
}
