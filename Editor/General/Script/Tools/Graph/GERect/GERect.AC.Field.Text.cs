/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GERect
    {
        #region 序列化属性 Field Text

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Field(Rect rect, string label, string content, GUIStyle style = null)
        {
            return EditorGUI.TextField(rect, label, content, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Field(Rect rect, GUIContent label, string content, GUIStyle style = null)
        {
            return EditorGUI.TextField(rect, label, content, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Field(Rect rect, string content, GUIStyle style = null)
        {
            return EditorGUI.TextField(rect, content, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Field(Vector2 pos, Vector2 size, string content, GUIStyle style = null)
        {
            return EditorGUI.TextField(new Rect(pos, size), content, style);
        }

        #endregion
    }
}
