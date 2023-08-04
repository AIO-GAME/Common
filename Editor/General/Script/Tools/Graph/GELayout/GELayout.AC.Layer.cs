/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        #region 选择层文本框 Field Layer

        /// <summary> 选择层文本框 FieldLayer </summary>
        /// <param name="layer">层数</param>
        /// <param name="options">排版格式</param>
        public static int Layer(int layer, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(layer, options);
        }

        /// <summary> 选择层文本框 FieldLayer </summary>
        /// <param name="layer">层数</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static int Layer(int layer, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(layer, style, options);
        }

        /// <summary> 选择层文本框 FieldLayer </summary>
        /// <param name="label">标签</param>
        /// <param name="layer">层数</param>
        /// <param name="options">排版格式</param>
        public static int Layer(string label, int layer, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, layer, options);
        }

        /// <summary> 选择层文本框 FieldLayer </summary>
        /// <param name="label">标签</param>
        /// <param name="layer">层数</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static int Layer(string label, int layer, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, layer, style, options);
        }

        /// <summary> 选择层文本框 FieldLayer </summary>
        /// <param name="label">标签</param>
        /// <param name="layer">层数</param>
        /// <param name="options">排版格式</param>
        public static int Layer(GUIContent label, int layer, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, layer, options);
        }

        /// <summary> 选择层文本框 FieldLayer </summary>
        /// <param name="label">标签</param>
        /// <param name="layer">层数</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static int Layer(GUIContent label, int layer, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.LayerField(label, layer, style, options);
        }

        #endregion
    }
}
