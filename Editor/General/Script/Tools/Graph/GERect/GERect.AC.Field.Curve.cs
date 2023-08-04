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
        #region 序列化属性 Field Animation Curve

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static AnimationCurve Field(Rect rect, AnimationCurve value)
        {
            return EditorGUI.CurveField(rect, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static AnimationCurve Field(Rect rect, string label, AnimationCurve value)
        {
            return EditorGUI.CurveField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static AnimationCurve Field(Rect rect, GUIContent label, AnimationCurve value)
        {
            return EditorGUI.CurveField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static AnimationCurve Field(Vector2 pos, Vector2 size, AnimationCurve value)
        {
            return EditorGUI.CurveField(new Rect(pos, size), value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static AnimationCurve Field(Vector2 pos, Vector2 size, string label, AnimationCurve value)
        {
            return EditorGUI.CurveField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static AnimationCurve Field(Vector2 pos, Vector2 size, GUIContent label, AnimationCurve value)
        {
            return EditorGUI.CurveField(new Rect(pos, size), label, value);
        }

        #endregion
    }
}
