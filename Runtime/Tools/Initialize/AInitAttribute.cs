﻿#region

using System;
using System.Runtime.CompilerServices;

#endregion

namespace AIO.UEditor
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class AInitAttribute : Attribute
    {
        public AInitAttribute(int order = 0
#if UNITY_EDITOR
                             ,
                              [CallerFilePath]   string filePath   = "",
                              [CallerLineNumber] int    lineNumber = 0
#endif
        )
        {
#if UNITY_EDITOR
            FilePath   = filePath.Replace("\\", "/");
            LineNumber = lineNumber;
#endif
            Order = order;
        }

        public AInitAttribute(EInitAttrMode mode,
                              int           order = 0
#if UNITY_EDITOR
                             ,
                              [CallerFilePath]   string filePath   = "",
                              [CallerLineNumber] int    lineNumber = 0
#endif
        )
        {
#if UNITY_EDITOR
            FilePath   = filePath.Replace("\\", "/");
            LineNumber = lineNumber + 1;
#endif
            Mode  = mode;
            Order = order;
        }

        public EInitAttrMode Mode { get; set; } = EInitAttrMode.Editor;

        public int Order { get; set; }
#if UNITY_EDITOR
        public string FilePath   { get; }
        public int    LineNumber { get; }
#endif
    }
}