/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using UnityEngine;

namespace UnityEditor
{
    public static partial class GELayout
    {
        #region 绘制轴对齐的边框 Bounds

        /// <summary> 绘制轴对齐的边框 </summary>
        /// <param name="value">表示轴对齐的边框</param>
        /// <param name="options">排版格式</param>
        public static Bounds Field(Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(value, options);
        }

        /// <summary> 绘制轴对齐的边框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">表示轴对齐的边框</param>
        /// <param name="options">排版格式</param>
        public static Bounds Field(string label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(label, value, options);
        }

        /// <summary> 绘制轴对齐的边框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">表示轴对齐的边框</param>
        /// <param name="options">排版格式</param>
        public static Bounds Field(GUIContent label, Bounds value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsField(label, value, options);
        }

        #endregion

        #region 绘制轴对齐的边框 BoundsInt

        /// <summary> 绘制轴对齐的边框 </summary>
        /// <param name="value">表示轴对齐的边框</param>
        /// <param name="options">排版格式</param>
        public static BoundsInt Field(BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(value, options);
        }

        /// <summary> 绘制轴对齐的边框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">表示轴对齐的边框</param>
        /// <param name="options">排版格式</param>
        public static BoundsInt Field(string label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(label, value, options);
        }

        /// <summary> 绘制轴对齐的边框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">表示轴对齐的边框</param>
        /// <param name="options">排版格式</param>
        public static BoundsInt Field(GUIContent label, BoundsInt value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.BoundsIntField(label, value, options);
        }

        #endregion
    }
}