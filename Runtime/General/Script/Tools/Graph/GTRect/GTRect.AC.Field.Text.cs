/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using UnityEngine;

namespace UnityEngine
{
    public partial class GURect
    {
        #region 序列化属性 Field Text

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Field(Rect rect, string label, GUIStyle style = null)
        {
            return GUI.TextField(rect, label, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Field(Vector2 pos, Vector2 size, string content, GUIStyle style = null)
        {
            return GUI.TextField(new Rect(pos, size), content, style);
        }

        #endregion
    }
}