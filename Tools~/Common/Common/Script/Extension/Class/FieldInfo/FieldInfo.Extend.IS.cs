﻿#region

using System.Reflection;
using System.Runtime.CompilerServices;

#endregion

namespace AIO
{
    /// <summary>
    ///     字段信息扩展
    /// </summary>
    public static class ExtendFieldInfo
    {
        /// <summary>
        ///     是否能写入
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool CanWrite(this FieldInfo fieldInfo)
        {
            return !(fieldInfo.IsInitOnly || fieldInfo.IsLiteral);
        }
    }
}