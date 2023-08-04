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
        #region 序列化属性 Field Double

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static double Field(Rect rect, double value, GUIStyle style = null)
        {
            return EditorGUI.DoubleField(rect, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static double Field(Rect rect, string label, double value, GUIStyle style = null)
        {
            return EditorGUI.DoubleField(rect, label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static double Field(Rect rect, GUIContent label, double value, GUIStyle style = null)
        {
            return EditorGUI.DoubleField(rect, label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static double Field(Vector2 pos, Vector2 size, double value, GUIStyle style = null)
        {
            return EditorGUI.DoubleField(new Rect(pos, size), value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static double Field(Vector2 pos, Vector2 size, string label, double value, GUIStyle style = null)
        {
            return EditorGUI.DoubleField(new Rect(pos, size), label, value, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static double Field(Vector2 pos, Vector2 size, GUIContent label, double value, GUIStyle style = null)
        {
            return EditorGUI.DoubleField(new Rect(pos, size), label, value, style);
        }

        #endregion
    }
}
