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
        #region 帮助框 HelpBox

        /// <summary> 帮助框 HelpBox </summary>
        /// <param name="message">内容</param>
        /// <param name="type">提示等级</param>
        public static void HelpBox(string message, MessageType type)
        {
            EditorGUILayout.HelpBox(message, type);
        }

        /// <summary> 帮助框 HelpBox </summary>
        /// <param name="message">内容</param>
        /// <param name="type">提示等级</param>
        /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分</param>
        public static void HelpBox(string message, MessageType type, bool wide)
        {
            EditorGUILayout.HelpBox(message, type, wide);
        }

        /// <summary> 帮助框 HelpBox </summary>
        /// <param name="content">内容</param>
        /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分</param>
        public static void HelpBox(GUIContent content, bool wide = true)
        {
            EditorGUILayout.HelpBox(content, wide);
        }

        /// <summary> 帮助框 HelpBox </summary>
        /// <param name="content">内容</param>
        /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分</param>
        public static void HelpBox(string content, bool wide = true)
        {
            EditorGUILayout.HelpBox(new GUIContent(content), wide);
        }

        /// <summary> 帮助框 HelpBox </summary>
        /// <param name="content">内容</param>
        /// <param name="wide">true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分</param>
        public static void HelpBox(Texture content, bool wide = true)
        {
            EditorGUILayout.HelpBox(new GUIContent(content), wide);
        }

        #endregion
    }
}