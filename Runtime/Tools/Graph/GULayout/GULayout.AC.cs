/*|✩ - - - - - |||
|||✩ Author:   ||| -> Sam Li
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEngine;

namespace AIO
{
    public partial class GULayout
    {
        /// <summary>
        /// 复制字符按钮
        /// </summary>
        public static void ButtonCopyText(string name, string content, GUIStyle style, float height = 30, float width = 100)
        {
            if (GUILayout.Button(name, style, GUILayout.Height(height), GUILayout.Width(width)))
                GUHelper.CopyTextAction(content);
        }

        /// <summary>
        /// 复制字符按钮
        /// </summary>
        public static void ButtonCopyText(string name, string content, float height = 30, float width = 100)
        {
            if (GUILayout.Button(name, GUILayout.Height(height), GUILayout.Width(width)))
                GUHelper.CopyTextAction(content);
        }
        
        /// <summary>
        /// 复制字符按钮
        /// </summary>
        public static void ButtonCopyText(GUIContent name, string content, GUIStyle style, float height = 30, float width = 100)
        {
            if (GUILayout.Button(name, style, GUILayout.Height(height), GUILayout.Width(width)))
                GUHelper.CopyTextAction(content);
        }

        /// <summary>
        /// 复制字符按钮
        /// </summary>
        public static void ButtonCopyText(GUIContent name, string content, float height = 30, float width = 100)
        {
            if (GUILayout.Button(name, GUILayout.Height(height), GUILayout.Width(width)))
                GUHelper.CopyTextAction(content);
        }
    }
}