/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GELayout
    {
        
        /// <summary>
        /// 复制字符按钮
        /// </summary>
        public static void ButtonCopyText(string name, float height, float width, string content, GUIStyle style = null)
        {
            if (Button(name, style, GUILayout.Height(height), GUILayout.Width(width))) GEHelper.CopyTextAction(content);
        }
    }
}