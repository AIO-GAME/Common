/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using UnityEngine;

namespace UnityEditor
{
    public partial class GELayout
    {
        #region Vector2

        /// <summary> 二维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector2 Field(GUIContent label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(label, value, options);
        }

        /// <summary> 二维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector2 Field(string label, Vector2 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2Field(label, value, options);
        }

        /// <summary> 二维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector2Int Field(GUIContent label, Vector2Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2IntField(label, value, options);
        }

        /// <summary> 二维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector2Int Field(string label, Vector2Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector2IntField(label, value, options);
        }

        #endregion

        #region Vector3

        /// <summary> 三维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector3 Field(GUIContent label, Vector3 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3Field(label, value, options);
        }

        /// <summary> 三维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector3 Field(string label, Vector3 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3Field(label, value, options);
        }

        /// <summary> 三维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector3Int Field(GUIContent label, Vector3Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3IntField(label, value, options);
        }

        /// <summary> 三维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector3Int Field(string label, Vector3Int value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector3IntField(label, value, options);
        }

        #endregion

        #region Vector4

        /// <summary> 四维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector4 Field(string label, Vector4 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector4Field(label, value, options);
        }

        /// <summary> 四维坐标文本框 </summary>
        /// <param name="label">标签</param>
        /// <param name="value">值</param>
        /// <param name="options">排版格式</param>
        public static Vector4 Field(GUIContent label, Vector4 value, params GUILayoutOption[] options)
        {
            return EditorGUILayout.Vector4Field(label, value, options);
        }

        #endregion
    }
}