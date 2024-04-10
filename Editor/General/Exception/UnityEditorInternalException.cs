#region

using System;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 这个类表示在访问Unity内部编辑器函数时发生错误时抛出的异常。
    /// </summary>
    public class UnityEditorInternalException : Exception
    {
        /// <inheritdoc/>
        public UnityEditorInternalException(in Exception innerException) : base(
            "An error occured while accessing internal Unity Editor functions. This might happen if Unity makes backward-incompatible changes in their newer versions of the editor.",
            innerException) { }
    }
}