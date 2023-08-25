/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEngine;

namespace AIO
{
    public static partial class GULayout
    {
        #region Text Area

        /// <summary> 文本区域 TextArea </summary>
        /// <param name="text">文本内容</param>
        /// <param name="options">排版格式</param>
        public static string Area(string text, params GUILayoutOption[] options)
        {
            return GUILayout.TextArea(text, options);
        }

        /// <summary> 文本区域 TextArea </summary>
        /// <param name="text">文本内容</param>
        /// <param name="style">显示风格</param>
        /// <param name="options">排版格式</param>
        public static string Area(string text, GUIStyle style, params GUILayoutOption[] options)
        {
            return GUILayout.TextArea(text, style, options);
        }

        #endregion
    }
}
