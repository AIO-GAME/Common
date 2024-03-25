using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        /// <remarks>
        /// </remarks>
        /// <param name="target">类型</param>
        /// <returns>
        /// namespace.class
        ///namespace.class generic`1
        /// </returns>
        public static string ToStrAlias(this Type target)
        {
            if (target is null) return "type is null";
            var str = GenericTypeToStr(target);
            if (string.IsNullOrEmpty(str)) return string.Empty;
            return str.Replace("System.Void", "void").Replace("System.String", "string").Replace("System.Int32", "int").
                Replace("System.Single", "float").Replace("System.Boolean", "bool").Replace("System.Object", "object").
                Replace("System.Byte", "byte").Replace("System.Char", "char").Replace("System.Double", "double").
                Replace("System.UInt32", "uint").Replace("System.UInt64", "ulong").Replace("System.UInt16", "ushort").
                Replace("System.Int64", "long").Replace("System.Int16", "short").Replace("System.SByte", "sbyte").
                Replace("System.Decimal", "decimal").Replace("&", "").Replace('+', '.');
            ;
        }

        private static string GenericTypeToStr(Type target)
        {
            if (target is null) return "type is null";
            string str;
            if (target.IsGenericType) // 判断是否为泛型类型 
            {
                var definition = target.GetGenericTypeDefinition();
                var genericArguments = target.GetGenericArguments();
                var genericTypeStr = definition.FullName;
                if (string.IsNullOrEmpty(genericTypeStr)) return definition.Name;
                genericTypeStr = genericTypeStr.Substring(0, genericTypeStr.IndexOf('`'));
                var genericArgumentsStr = string.Join(", ",
                    genericArguments.Select(genericArgument => genericArgument.IsGenericParameter
                        ? genericArgument.Name
                        : GenericTypeToStr(genericArgument)));
                str = $"{genericTypeStr}<{genericArgumentsStr}>";
            }
            else if (target.IsArray)
            {
                if (target.ContainsGenericParameters)
                {
                    str = target.Name;
                }

                else str = target.GetElementType().ToStrAlias() + "[]";
            }
            else if (target.IsByRef)
            {
                str = target.GetElementType().ToStrAlias() + "&";
            }
            else if (target.IsPointer)
            {
                str = target.GetElementType().ToStrAlias() + "*";
            }
            else str = target.FullName;

            return str;
        }

        /// <summary>
        /// 转化为标准字符串
        /// </summary>
        /// <param name="parameter">参数信息</param>
        /// <returns>
        /// [in Type name]
        /// [out Type name]
        /// [params Type[] name]
        /// </returns>
        public static string ToStrAlias(this ParameterInfo parameter)
        {
            var genericBuilder = new StringBuilder();
            // 获取参数的特性 param out ref 等等
            if (parameter.IsIn) genericBuilder.Append("in ");
            else if (parameter.IsOut) genericBuilder.Append("out ");
            else if (parameter.ParameterType.IsByRef) genericBuilder.Append("ref ");
            else if (parameter.GetCustomAttribute<ParamArrayAttribute>() != null)
                genericBuilder.Append("params ");

            // 判断类型是否为泛型参数

            // 判断是否为泛型参数
            genericBuilder.Append(parameter.ParameterType.IsGenericParameter
                ? parameter.ParameterType.Name
                : parameter.ParameterType.ToStrAlias());

            return genericBuilder.Append(' ').Append(parameter.Name.ToLower()).ToString();
        }


        /// <summary>
        /// 转化为标准字符串
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string ToStrAlias(this MethodInfo method)
        {
            if (method is null) return "method is null";
            var str = new StringBuilder();

            str.Append(method.ReflectedType.ToStrAlias());
            str.Append(method.IsPublic ? " public" : " private");
            str.Append(method.IsStatic ? " static" : " ");
            str.Append(method.ReturnType == typeof(void) ? " void " : $" {method.ReturnType.ToStrAlias()} ");
            str.Append(method.Name);

            if (method.IsGenericMethod)
            {
                // 判断是否为泛型函数
                var genericBuilder = new StringBuilder();
                var genericArguments = method.GetGenericArguments();
                foreach (var argument in genericArguments)
                {
                    if (argument is null) continue;
                    genericBuilder.Append(argument.Name).Append(", ");
                }

                if (genericBuilder.Length != 0)
                {
                    str.Append('<');
                    str.Append(genericBuilder.ToString().Trim().TrimEnd(','));
                    str.Append(">(");
                    str.Append(string.Join(", ", method.GetParameters().Select(info => info.ToStrAlias())).Trim());
                    str.Append(") ");
                    var temp = method.
                        MakeGenericMethod(genericArguments).
                        GetGenericArguments().
                        Select(g =>
                        {
                            var tempBuilder = new List<string>();


                            if (g.GenericParameterAttributes.HasFlag(
                                    GenericParameterAttributes.ReferenceTypeConstraint))
                            {
                                tempBuilder.Add("class");
                            }
                            else if (g.GenericParameterAttributes.HasFlag(
                                         GenericParameterAttributes.NotNullableValueTypeConstraint))
                            {
                                tempBuilder.Add("struct");
                            }


                            var constraints = g.GetGenericParameterConstraints();
                            if (constraints.Length > 0)
                            {
                                tempBuilder.AddRange(constraints.Select(c => c.ToStrAlias()));
                            }

                            var temp = string.Join(", ", tempBuilder);
                            if (g.GenericParameterAttributes.HasFlag(
                                    GenericParameterAttributes.NotNullableValueTypeConstraint))
                            {
                                temp = temp.Replace("System.ValueType", "");
                            }
                            else if (g.GenericParameterAttributes.HasFlag(
                                         GenericParameterAttributes.DefaultConstructorConstraint))
                            {
                                temp = string.Concat(temp, "new()");
                            }

                            return string.Concat("where ", g.Name, " : ", temp).TrimEnd(',', ' ');
                        });
                    str.Append(string.Join(" ", temp).TrimEnd(',', ' '));
                }
            }
            else
            {
                str.Append('(');
                str.Append(string.Join(", ", method.GetParameters().Select(p => p.ParameterType.ToStrAlias())));
                str.Append(')');
            }

            return str.ToString();
        }
    }
}