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
        #region 序列化属性 Field Bounds

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Bounds Field(Rect rect, Bounds value)
        {
            return EditorGUI.BoundsField(rect, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Bounds Field(Rect rect, string label, Bounds value)
        {
            return EditorGUI.BoundsField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Bounds Field(Rect rect, GUIContent label, Bounds value)
        {
            return EditorGUI.BoundsField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Bounds Field(Vector2 pos, Vector2 size, Bounds value)
        {
            return EditorGUI.BoundsField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Bounds Field(Vector2 pos, Vector2 size, string label, Bounds value)
        {
            return EditorGUI.BoundsField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Bounds Field(Vector2 pos, Vector2 size, GUIContent label, Bounds value)
        {
            return EditorGUI.BoundsField(new Rect(pos, size), label, value);
        }

        #endregion

        #region 序列化属性 Field BoundsInt

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static BoundsInt Field(Rect rect, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(rect, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static BoundsInt Field(Rect rect, string label, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static BoundsInt Field(Rect rect, GUIContent label, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static BoundsInt Field(Vector2 pos, Vector2 size, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static BoundsInt Field(Vector2 pos, Vector2 size, string label, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static BoundsInt Field(Vector2 pos, Vector2 size, GUIContent label, BoundsInt value)
        {
            return EditorGUI.BoundsIntField(new Rect(pos, size), label, value);
        }

        #endregion
    }
}