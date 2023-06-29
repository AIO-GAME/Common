/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

namespace UnityEngine
{
    public partial class GULayout
    {
        #region 密码文本框 Password Field

        /// <summary> 密码文本框 FieldPassword </summary>
        public static string Password(string password, char mask, params GUILayoutOption[] options)
        {
            return GUILayout.PasswordField(password, mask, options);
        }


        /// <summary>
        ///   <para>Make a text field where the user can enter a password.</para>
        /// </summary>
        public static string Password(
            string password,
            char maskChar,
            int maxLength,
            params GUILayoutOption[] options)
        {
            return GUILayout.PasswordField(password, maskChar, maxLength, GUI.skin.textField, options);
        }

        /// <summary>
        ///   <para>Make a text field where the user can enter a password.</para>
        /// </summary>
        public static string Password(
            string password,
            char maskChar,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            return GUILayout.PasswordField(password, maskChar, -1, style, options);
        }

        /// <summary>
        ///   <para>Make a text field where the user can enter a password.</para>
        /// </summary>
        public static string Password(
            Rect position,
            string password,
            char maskChar,
            int maxLength,
            GUIStyle style)
        {
            return GUI.PasswordField(position, password, maskChar, maxLength, style);
        }

        #endregion
    }
}