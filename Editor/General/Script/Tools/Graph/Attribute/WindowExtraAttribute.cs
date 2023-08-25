/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;

namespace AIO.UEditor
{
    /// <summary>
    /// 额外的窗口
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WindowExtraAttribute : Attribute
    {
        /// <summary>
        /// 组
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order { get; }

        /// <inheritdoc />
        public WindowExtraAttribute(string group, int order)
        {
            Group = group;
            Order = order;
        }

        /// <inheritdoc />
        public WindowExtraAttribute(string group)
        {
            Group = group;
        }
    }
}
