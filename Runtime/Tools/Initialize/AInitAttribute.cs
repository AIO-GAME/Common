#region

using System;
#if UNITY_EDITOR
using System.Runtime.CompilerServices;
#endif

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// 初始化属性
    /// </summary>
    /// <c>使用方式</c>
    /// <code>
    /// [AInit(EInitAttrMode.Editor)] : 编辑器启动
    /// [AInit(EInitAttrMode.RuntimeAfterAssembliesLoaded)] : 程序集加载
    /// [AInit(0)] : 优先级
    /// </code>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class AInitAttribute : Attribute
    {
#if UNITY_EDITOR
        public string FilePath   { get; }
        public int    LineNumber { get; }

        public AInitAttribute(int order = 0, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            FilePath   = filePath.Replace("\\", "/");
            LineNumber = lineNumber;
            Order      = order;
        }

        public AInitAttribute(EInitAttrMode mode, int order = 0, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            FilePath   = filePath.Replace("\\", "/");
            LineNumber = lineNumber + 1;
            Mode       = mode;
            Order      = order;
        }
#else
        public AInitAttribute(int order = 0) { Order = order; }
        public AInitAttribute(EInitAttrMode mode, int order = 0)
        {
            Mode = mode;
            Order = order;
        }
#endif

        public EInitAttrMode Mode { get; set; } = EInitAttrMode.Editor;

        public int Order { get; set; }
    }
}