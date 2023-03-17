/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    /// <summary>
    /// 打开方式
    /// </summary>
    public enum EPrVerb
    {
        /// <summary>
        /// 编辑
        /// </summary>
        Edit,

        /// <summary>
        /// 打开
        /// </summary>
        Open,

        /// <summary>
        /// 打开只读
        /// </summary>
        OpenAsReadOnly,

        /// <summary>
        /// 打印
        /// </summary>
        Print,

        /// <summary>
        /// Admin身份运行
        /// </summary>
        RunAs,

        /// <summary>
        /// User身份运行
        /// </summary>
        RunAsUser
    }
}