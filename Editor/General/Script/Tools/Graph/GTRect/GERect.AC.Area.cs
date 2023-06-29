/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using UnityEngine;

namespace UnityEditor
{
    public partial class GERect
    {
        #region 序列化属性 Text Area

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Area(Rect rect, string content, GUIStyle style)
        {
            return EditorGUI.TextArea(rect, content, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Area(Rect rect, string content)
        {
            return EditorGUI.TextArea(rect, content, EditorStyles.textArea);
        }

        #endregion
    }
}