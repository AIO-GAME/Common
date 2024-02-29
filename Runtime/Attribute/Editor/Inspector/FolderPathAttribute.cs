using System;
using System.Diagnostics;

namespace AIO.UEditor
{
    /// <summary>
    /// 文件夹路径检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public sealed class FolderPathAttribute : InspectorAttribute
    {
    }
}