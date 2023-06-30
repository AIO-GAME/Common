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
        #region 序列化属性 Field Color

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Color Field(Rect rect, Color value)
        {
            return EditorGUI.ColorField(rect, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Color Field(Rect rect, string label, Color value)
        {
            return EditorGUI.ColorField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Color Field(Rect rect, GUIContent label, Color value)
        {
            return EditorGUI.ColorField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Color Field(Vector2 pos, Vector2 size, Color value)
        {
            return EditorGUI.ColorField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Color Field(Vector2 pos, Vector2 size, string label, Color value)
        {
            return EditorGUI.ColorField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Color Field(Vector2 pos, Vector2 size, GUIContent label, Color value)
        {
            return EditorGUI.ColorField(new Rect(pos, size), label, value);
        }

        #endregion
    }
}