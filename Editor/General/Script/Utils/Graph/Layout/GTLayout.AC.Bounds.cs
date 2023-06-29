/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using UnityEngine;

namespace UnityEditor
{
    public static partial class GTLayout
    {
        /// <summary>
        /// 数值绘制函数
        /// </summary>
        public partial class AC
        {
            /* ----------------Bounds---------------- */

            #region 绘制轴对齐的边框 Bounds

            /// <summary> 绘制轴对齐的边框 </summary>
            /// <param name="value">表示轴对齐的边框</param>
            /// <param name="options">排版格式</param>
            public static Bounds BoundsField(Bounds value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.BoundsField(value, options);
            }

            /// <summary> 绘制轴对齐的边框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">表示轴对齐的边框</param>
            /// <param name="options">排版格式</param>
            public static Bounds BoundsField(string label, Bounds value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.BoundsField(label, value, options);
            }

            /// <summary> 绘制轴对齐的边框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">表示轴对齐的边框</param>
            /// <param name="options">排版格式</param>
            public static Bounds BoundsField(GUIContent label, Bounds value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.BoundsField(label, value, options);
            }

            #endregion

            #region 绘制轴对齐的边框 BoundsInt

            /// <summary> 绘制轴对齐的边框 </summary>
            /// <param name="value">表示轴对齐的边框</param>
            /// <param name="options">排版格式</param>
            public static BoundsInt BoundsField(BoundsInt value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.BoundsIntField(value, options);
            }

            /// <summary> 绘制轴对齐的边框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">表示轴对齐的边框</param>
            /// <param name="options">排版格式</param>
            public static BoundsInt BoundsField(string label, BoundsInt value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.BoundsIntField(label, value, options);
            }

            /// <summary> 绘制轴对齐的边框 </summary>
            /// <param name="label">标签</param>
            /// <param name="value">表示轴对齐的边框</param>
            /// <param name="options">排版格式</param>
            public static BoundsInt BoundsField(GUIContent label, BoundsInt value, params GUILayoutOption[] options)
            {
                return EditorGUILayout.BoundsIntField(label, value, options);
            }

            #endregion
        }
    }
}