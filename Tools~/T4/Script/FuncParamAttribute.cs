#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 函数参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class FuncParamAttribute : Attribute
    {
        /// <summary>
        /// 区块组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 是否返回数组
        /// </summary>
        public bool IsArray { get; set; }
    }
}