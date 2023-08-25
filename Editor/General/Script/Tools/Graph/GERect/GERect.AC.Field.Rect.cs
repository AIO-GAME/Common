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
        #region 序列化属性 Field Rect

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Rect Field(Rect rect, Rect value)
        {
            return EditorGUI.RectField(rect, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Rect Field(Rect rect, string label, Rect value)
        {
            return EditorGUI.RectField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Rect Field(Rect rect, GUIContent label, Rect value)
        {
            return EditorGUI.RectField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Rect Field(Vector2 pos, Vector2 size, Rect value)
        {
            return EditorGUI.RectField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Rect Field(Vector2 pos, Vector2 size, string label, Rect value)
        {
            return EditorGUI.RectField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Rect Field(Vector2 pos, Vector2 size, GUIContent label, Rect value)
        {
            return EditorGUI.RectField(new Rect(pos, size), label, value);
        }

        #endregion

        #region 序列化属性 Field RectInt

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static RectInt Field(Rect rect, RectInt value)
        {
            return EditorGUI.RectIntField(rect, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static RectInt Field(Rect rect, string label, RectInt value)
        {
            return EditorGUI.RectIntField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static RectInt Field(Rect rect, GUIContent label, RectInt value)
        {
            return EditorGUI.RectIntField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static RectInt Field(Vector2 pos, Vector2 size, RectInt value)
        {
            return EditorGUI.RectIntField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static RectInt Field(Vector2 pos, Vector2 size, string label, RectInt value)
        {
            return EditorGUI.RectIntField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static RectInt Field(Vector2 pos, Vector2 size, GUIContent label, RectInt value)
        {
            return EditorGUI.RectIntField(new Rect(pos, size), label, value);
        }

        #endregion
    }
}
