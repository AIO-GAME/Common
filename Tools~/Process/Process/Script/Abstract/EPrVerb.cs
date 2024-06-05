namespace AIO
{
    using System.ComponentModel;

    /// <summary>
    /// 打开方式
    /// </summary>
    public enum EPrVerb
    {
        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit,

        /// <summary>
        /// 打开
        /// </summary>
        [Description("打开")]
        Open,

        /// <summary>
        /// 打开只读
        /// </summary>
        [Description("打开只读")]
        OpenAsReadOnly,

        /// <summary>
        /// 打印
        /// </summary>
        [Description("打印")]
        Print,

        /// <summary>
        /// Admin身份运行
        /// </summary>
        [Description("Admin身份运行")]
        RunAs,

        /// <summary>
        /// User身份运行
        /// </summary>
        [Description("User身份运行")]
        RunAsUser
    }
}