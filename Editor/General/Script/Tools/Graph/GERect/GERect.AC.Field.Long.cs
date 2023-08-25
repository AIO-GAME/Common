/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GERect
    {
        #region 序列化属性 Field long

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static long Field(Rect rect, long value, GUIStyle style = null)
        {
            return EditorGUI.LongField(rect, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static long Field(Rect rect, string label, long value, GUIStyle style = null)
        {
            return EditorGUI.LongField(rect, label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static long Field(Rect rect, GUIContent label, long value, GUIStyle style = null)
        {
            return EditorGUI.LongField(rect, label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static long Field(Vector2 pos, Vector2 size, long value, GUIStyle style = null)
        {
            return EditorGUI.LongField(new Rect(pos, size), value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static long Field(Vector2 pos, Vector2 size, string label, long value, GUIStyle style = null)
        {
            return EditorGUI.LongField(new Rect(pos, size), label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static long Field(Vector2 pos, Vector2 size, GUIContent label, long value, GUIStyle style = null)
        {
            return EditorGUI.LongField(new Rect(pos, size), label, value, style);
        }

        #endregion
    }
}
