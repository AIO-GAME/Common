/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AIO
{
    using System;

    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static partial class EnumExtend
    {
        /// <summary>
        /// 获取属性值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            if (value == null) throw new ArgumentException("value");
            var description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes = (T[])fieldInfo.GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? attributes[0] : null;
        }

        /// <summary>
        /// Gets attributes on an enum member, eg. enum E { [Attr] A }
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetAttributeOfEnumMember<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Cast<T>();
        }
    }
}