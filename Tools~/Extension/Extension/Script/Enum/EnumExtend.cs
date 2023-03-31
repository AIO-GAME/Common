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
    using System.ComponentModel;
    using System.Reflection;

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
            var fieldInfo = value.GetType().GetField(value.ToString());
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

        /// <summary>
        /// 获取指定描述列表
        /// </summary>
        public static Dictionary<T, string> GetDescriptionDic<T>(this T value) where T : struct, Enum
        {
            var type = typeof(T);
            var DescriptionDic = new Dictionary<T, string>();
            var index = 0;
            var values = Enum.GetValues(type);
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute(typeof(DescriptionAttribute), false);
                if (attribute is DescriptionAttribute description)
                    DescriptionDic.Add((T)values.GetValue(index++), description.Description);
                else DescriptionDic.Add((T)values.GetValue(index++), field.Name);
            }

            return DescriptionDic;
        }

        /// <summary>
        /// 获取自定义属性值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetDescription<T>(this T value) where T : struct, Enum
        {
            var description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes = fieldInfo.GetCustomAttribute<DescriptionAttribute>(false);
            if (attributes != null) return attributes.Description;
            return description;
        }
    }
}