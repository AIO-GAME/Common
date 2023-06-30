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
        #region 序列化属性 Field Vector2

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector2 Field(Rect rect, string label, Vector2 value)
        {
            return EditorGUI.Vector2Field(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector2 Field(Rect rect, GUIContent label, Vector2 value)
        {
            return EditorGUI.Vector2Field(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector2 Field(Vector2 pos, Vector2 size, string label, Vector2 value)
        {
            return EditorGUI.Vector2Field(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector2 Field(Vector2 pos, Vector2 size, GUIContent label, Vector2 value)
        {
            return EditorGUI.Vector2Field(new Rect(pos, size), label, value);
        }

        #endregion

        #region 序列化属性 Field Vector2Int

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector2Int Field(Rect rect, string label, Vector2Int value)
        {
            return EditorGUI.Vector2IntField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector2Int Field(Rect rect, GUIContent label, Vector2Int value)
        {
            return EditorGUI.Vector2IntField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector2Int Field(Vector2 pos, Vector2 size, string label, Vector2Int value)
        {
            return EditorGUI.Vector2IntField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector2Int Field(Vector2 pos, Vector2 size, GUIContent label, Vector2Int value)
        {
            return EditorGUI.Vector2IntField(new Rect(pos, size), label, value);
        }

        #endregion

        #region 序列化属性 Field Vector3

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector3 Field(Rect rect, string label, Vector3 value)
        {
            return EditorGUI.Vector3Field(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector3 Field(Rect rect, GUIContent label, Vector3 value)
        {
            return EditorGUI.Vector3Field(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector3 Field(Vector2 pos, Vector2 size, string label, Vector3 value)
        {
            return EditorGUI.Vector3Field(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector3 Field(Vector2 pos, Vector2 size, GUIContent label, Vector3 value)
        {
            return EditorGUI.Vector3Field(new Rect(pos, size), label, value);
        }

        #endregion

        #region 序列化属性 Field Vector3Int

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector3Int Field(Rect rect, string label, Vector3Int value)
        {
            return EditorGUI.Vector3IntField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector3Int Field(Rect rect, GUIContent label, Vector3Int value)
        {
            return EditorGUI.Vector3IntField(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector3Int Field(Vector2 pos, Vector2 size, string label, Vector3Int value)
        {
            return EditorGUI.Vector3IntField(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector3Int Field(Vector2 pos, Vector2 size, GUIContent label, Vector3Int value)
        {
            return EditorGUI.Vector3IntField(new Rect(pos, size), label, value);
        }

        #endregion

        #region 序列化属性 Field Vector4

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector4 Field(Rect rect, string label, Vector4 value)
        {
            return EditorGUI.Vector4Field(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector4 Field(Rect rect, GUIContent label, Vector4 value)
        {
            return EditorGUI.Vector4Field(rect, label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector4 Field(Vector2 pos, Vector2 size, string label, Vector4 value)
        {
            return EditorGUI.Vector4Field(new Rect(pos, size), label, value);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static Vector4 Field(Vector2 pos, Vector2 size, GUIContent label, Vector4 value)
        {
            return EditorGUI.Vector4Field(new Rect(pos, size), label, value);
        }

        #endregion
    }
}