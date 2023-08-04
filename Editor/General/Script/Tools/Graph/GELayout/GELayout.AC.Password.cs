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
        #region 密码文本框 Password Field

        /// <summary> 密码文本框 FieldPassword </summary>
        /// <param name="password">遮掩码</param>
        /// <param name="options">排版格式</param>
        public static string Password(string password, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(password, options);
        }

        /// <summary> 密码文本框 FieldPassword </summary>
        /// <param name="password">遮掩码</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        public static string Password(string password, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(password, style, options);
        }


        /// <summary> 密码文本框 FieldPassword </summary>
        /// <param name="password">遮掩码</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static string Password(string label, string password, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, password, options);
        }

        /// <summary> 密码文本框 FieldPassword </summary>
        /// <param name="password">遮掩码</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        /// <param name="label">标签</param>
        public static string Password(string label, string password, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, password, style, options);
        }

        /// <summary> 密码文本框 FieldPassword </summary>
        /// <param name="password">遮掩码</param>
        /// <param name="options">排版格式</param>
        /// <param name="label">标签</param>
        public static string Password(GUIContent label, string password, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, password, options);
        }

        /// <summary> 密码文本框 FieldPassword </summary>
        /// <param name="password">遮掩码</param>
        /// <param name="options">排版格式</param>
        /// <param name="style">显示风格</param>
        /// <param name="label">标签</param>
        public static string Password(GUIContent label, string password, GUIStyle style, params GUILayoutOption[] options)
        {
            return EditorGUILayout.PasswordField(label, password, style, options);
        }

        #endregion
    }
}
