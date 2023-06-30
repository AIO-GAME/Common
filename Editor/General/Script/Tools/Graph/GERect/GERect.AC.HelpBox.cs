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
        #region 帮助框 HelpBox

        /// <summary>
        /// 帮助框
        /// </summary>
        public static void HelpBox(Rect rect, string content)
        {
            EditorGUI.HelpBox(rect, content, MessageType.None);
        }

        /// <summary>
        /// 帮助框
        /// </summary>
        public static void HelpBox(Vector2 pos, Vector2 size, string content)
        {
            EditorGUI.HelpBox(new Rect(pos + size / 2, size), content, MessageType.None);
        }

        /// <summary>
        /// 帮助框
        /// </summary>
        public static void HelpBox(Rect rect, string content, MessageType type)
        {
            EditorGUI.HelpBox(rect, content, type);
        }

        /// <summary>
        /// 帮助框
        /// </summary>
        public static void HelpBox(Vector2 pos, Vector2 size, string content, MessageType type)
        {
            EditorGUI.HelpBox(new Rect(pos - size / 2, size), content, type);
        }

        #endregion
    }
}