#region

using System;
using System.Diagnostics;

#endregion

namespace AIO.UEditor
{
    /// <summary>
    /// Class类型检视器（支持 string 类型）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field), Conditional("UNITY_EDITOR")]
    public sealed class ClassTypeAttribute : InspectorAttribute
    {
        /// <summary>
        /// Class类型检视器（支持 string 类型）
        /// </summary>
        /// <param name="parentClass">检索类型的父类</param>
        /// <param name="isIgnoreAbstract">是否忽略抽象类</param>
        /// <param name="isOnlyRuntime">是否仅检索运行时程序集，否则检索所有程序集</param>
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