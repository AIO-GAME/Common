/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-29
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEngine;

namespace AIO
{
    public partial class GURect
    {
        #region 序列化属性 Text Area

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Area(Rect rect, string content, GUIStyle style)
        {
            return GUI.TextArea(rect, content, style);
        }

        /// <summary>
        /// 创建一块文本输入区域
        /// </summary>
        public static string Area(Rect rect, string content)
        {
            return GUI.TextArea(rect, content);
        }

        #endregion
    }
}
