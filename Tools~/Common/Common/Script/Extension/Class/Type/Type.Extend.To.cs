using System;
using System.Linq;

namespace AIO
{
    public static partial class ExtendType
    {
        /// <summary>
        /// 使用 as 强转目标
        /// </summary>
        /// <typeparam name="T">强转的类型</typeparam>
        /// <param name="target">强转的对象</param>
        /// <returns>转换后的对象</returns>
        public static T To<T>(this object target) where T : class
        {
            return target as T;
        }

        /// <summary>
        /// 转化为标准字符串
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToStr(this Type target)
        {
            return GenericTypeToStr(target).Replace('+', '.');
        }

        /// <summary>
        /// 转化为标准字符串
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToStrAlias(this Type target)
        {
            return GenericTypeToStr(target).
                Replace("System.Void", "void").
                Replace("System.String", "string").
                Replace("System.Int32", "int").
                Replace("System.Single", "float").
                Replace("System.Boolean", "bool").
                Replace("System.Object", "object").
                Replace("System.Byte", "byte").
                Replace("System.Char", "char").
                Replace("System.Double", "double").
                Replace("System.UInt32", "uint").
                Replace("System.UInt64", "ulong").
                Replace("System.UInt16", "ushort").
                Replace("System.Int64", "long").
                Replace("System.Int16", "short").
                Replace("System.SByte", "sbyte").
                Replace("System.Decimal", "decimal").
                Replace('+', '.');
        }

        private static string GenericTypeToStr(Type type)
        {
            var str = type.FullName;
            if (string.IsNullOrEmpty(str)) return type.FullName;
            if (type.IsGenericType)
            {
                var genericTypeStr = GenericTypeToStr(type.GetGenericTypeDefinition());
                if (!string.IsNullOrEmpty(genericTypeStr))
                {
                    var genericArguments = type.GetGenericArguments();
                    genericTypeStr = genericTypeStr.Substring(0, genericTypeStr.IndexOf('`'));
                    var genericArgumentsStr = string.Join(", ", genericArguments.Select((genericArgument) =>
                    {
                        if (genericArgument.IsGenericParameter)
                        {
                            return genericArgument.Name;
                        }

                        return GenericTypeToStr(genericArgument);
                    }));
                    str = $"{genericTypeStr}<{genericArgumentsStr}>";
                }
            }

            return str;
        }
    }
}