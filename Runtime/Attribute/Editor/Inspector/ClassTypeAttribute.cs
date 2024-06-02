#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEngine
{
    /// <summary>
    /// Class类型检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class ClassTypeAttribute : InspectorAttribute
    {
        public ClassTypeAttribute(Type parentClass, bool isIgnoreAbstract = true, bool isOnlyRuntime = true)
        {
            ParentClass      = parentClass;
            IsIgnoreAbstract = isIgnoreAbstract;
            IsOnlyRuntime    = isOnlyRuntime;
        }

        public Type ParentClass      { get; private set; }
        public bool IsIgnoreAbstract { get; private set; }
        public bool IsOnlyRuntime    { get; private set; }
    }
}