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
        public static void ButtonCopyText(string name, float height, float width, string content, GUIStyle style = null)
        {
            if (GUILayout.Button(name, style, GUILayout.Height(height), GUILayout.Width(width)))
                GUHelper.CopyTextAction(content);
        }
    }
}