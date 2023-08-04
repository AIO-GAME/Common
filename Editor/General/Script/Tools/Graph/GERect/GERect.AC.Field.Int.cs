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
        #region 序列化属性 Field int

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static int Field(Rect rect, int value, GUIStyle style = null)
        {
            return EditorGUI.IntField(rect, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static int Field(Rect rect, string label, int value, GUIStyle style = null)
        {
            return EditorGUI.IntField(rect, label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static int Field(Rect rect, GUIContent label, int value, GUIStyle style = null)
        {
            return EditorGUI.IntField(rect, label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static int Field(Vector2 pos, Vector2 size, int value, GUIStyle style = null)
        {
            return EditorGUI.IntField(new Rect(pos, size), value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static int Field(Vector2 pos, Vector2 size, string label, int value, GUIStyle style = null)
        {
            return EditorGUI.IntField(new Rect(pos, size), label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static int Field(Vector2 pos, Vector2 size, GUIContent label, int value, GUIStyle style = null)
        {
            return EditorGUI.IntField(new Rect(pos, size), label, value, style);
        }

        #endregion
    }
}
