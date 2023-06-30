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
        #region 序列化属性 Field Gradient

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Gradient Field(Rect rect, Gradient value)
        {
            return EditorGUI.GradientField(rect, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Gradient Field(Rect rect, string label, Gradient value)
        {
            return EditorGUI.GradientField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Gradient Field(Rect rect, GUIContent label, Gradient value)
        {
            return EditorGUI.GradientField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Gradient Field(Vector2 pos, Vector2 size, Gradient value)
        {
            return EditorGUI.GradientField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Gradient Field(Vector2 pos, Vector2 size, string label, Gradient value)
        {
            return EditorGUI.GradientField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Gradient Field(Vector2 pos, Vector2 size, GUIContent label, Gradient value)
        {
            return EditorGUI.GradientField(new Rect(pos, size), label, value);
        }

        #endregion
    }
}