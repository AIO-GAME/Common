/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEngine;

namespace AIO
{
    public partial class GULayout
    {
        /// <summary>
        ///   <para>Make an on/off toggle button.</para>
        /// </summary>
        public static bool Toggle(bool value, Texture image, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, image, GUI.skin.toggle, options);
        }

        /// <summary>
        ///   <para>Make an on/off toggle button.</para>
        /// </summary>
        public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, text, GUI.skin.toggle, options);
        }

        /// <summary>
        ///   <para>Make an on/off toggle button.</para>
        /// </summary>
        public static bool Toggle(bool value, GUIContent content, params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, content, GUI.skin.toggle, options);
        }

        /// <summary>
        ///   <para>Make an on/off toggle button.</para>
        /// </summary>
        public static bool Toggle(
            bool value,
            Texture image,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, image, style, options);
        }

        /// <summary>
        ///   <para>Make an on/off toggle button.</para>
        /// </summary>
        public static bool Toggle(
            bool value,
            string text,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, text, style, options);
        }

        /// <summary>
        ///   <para>Make an on/off toggle button.</para>
        /// </summary>
        public static bool Toggle(
            bool value,
            GUIContent content,
            GUIStyle style,
            params GUILayoutOption[] options)
        {
            return GUILayout.Toggle(value, content, style, options);
        }
    }
}
