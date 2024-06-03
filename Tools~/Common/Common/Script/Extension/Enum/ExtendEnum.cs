#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

#endregion

namespace AIO
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class ExtendEnum
    {
        /// <summary>
        /// 获取属性值
        /// </summary>
        public static T GetAttribute<T>(this Enum value)
        where T : Attribute
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (T[])fieldInfo.GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? attributes[0] : null;
        }

        /// <summary>
        /// Gets attributes on an enum member, eg. enum E { [Attr] A }
        /// </summary>
        public static IEnumerable<T> GetAttributeOfEnumMember<T>(this Enum enumVal)
        where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Cast<T>();
        }

        /// <summary>
        /// 获取指定描述列表
        /// </summary>
        public static Dictionary<T, string> GetDescriptionDic<T>(this T value)
        where T : struct, Enum
        {
            var type = typeof(T);
            var descriptionDic = new Dictionary<T, string>();
            var values = Enum.GetNames(type);
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var attribute = field.GetCustomAttribute(typeof(DescriptionAttribute), false);

                if (attribute is DescriptionAttribute description)
                    descriptionDic.Add((T)field.GetValue(null), description.Description);
                else descriptionDic.Add((T)field.GetValue(null), field.Name);
            }

            return descriptionDic;
        }

        /// <summary>
        /// 获取自定义属性值
        /// </summary>
        public static string GetDescription<T>(this T value)
        where T : struct, Enum
        {
            var description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attributes = fieldInfo.GetCustomAttribute<DescriptionAttribute>(false);
            if (attributes != null) return attributes.Description;
            return description;
        }
    }
}