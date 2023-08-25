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
            #region 序列化属性 Field float

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static float Field(Rect rect, float value, GUIStyle style = null)
            {
                return EditorGUI.FloatField(rect, value, style);
            }

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static float Field(Rect rect, string label, float value, GUIStyle style = null)
            {
                return EditorGUI.FloatField(rect, label, value, style);
            }

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static float Field(Rect rect, GUIContent label, float value, GUIStyle style = null)
            {
                return EditorGUI.FloatField(rect, label, value, style);
            }

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static float Field(Vector2 pos, Vector2 size, float value, GUIStyle style = null)
            {
                return EditorGUI.FloatField(new Rect(pos, size), value, style);
            }

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static float Field(Vector2 pos, Vector2 size, string label, float value, GUIStyle style = null)
            {
                return EditorGUI.FloatField(new Rect(pos, size), label, value, style);
            }

            /// <summary>
            /// 创建一块文本输入区域
            /// </summary>
            public static float Field(Vector2 pos, Vector2 size, GUIContent label, float value, GUIStyle style = null)
            {
                return EditorGUI.FloatField(new Rect(pos, size), label, value, style);
            }

            #endregion
    }
}
