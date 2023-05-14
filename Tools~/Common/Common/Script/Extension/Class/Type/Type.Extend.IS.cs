using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AIO
{
    public partial class TypeExtend
    {
        private static readonly HashSet<Type> numericTypes = new HashSet<Type>
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        /// <summary>
        /// 确定给定的源 Type 是否可以向上转换为指定的目标 Type。
        /// </summary>
        /// <param name="source">要检查向上转换的 Type。</param>
        /// <param name="destination">source Type 要转换成的 Type。</param>
        /// <returns>如果从源 Type 到目标 Type 存在向上转换，则为 true；否则为 false。</returns>
        public static bool IsUpcast(this Type source, in Type destination)
        {
            return destination.IsAssignableFrom(source);
        }

        /// <summary>
        /// 确定给定的源 Type 是否可以向下转换为指定的目标 Type。
        /// </summary>
        /// <param name="source">要检查向下转换的 Type。</param>
        /// <param name="destination">source Type 要转换成的 Type。</param>
        /// <returns>如果从源 Type 到目标 Type 存在向下转换，则为 true；否则为 false。</returns>
        public static bool IsDowncast(this Type source, in Type destination)
        {
            return source.IsAssignableFrom(destination);
        }

        /// <summary>
        /// 判断是否为数据类型
        /// </summary>
        public static bool IsNumeric(this Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            return numericTypes.Contains(type);
        }

        /// <summary>
        /// 检查类型是否可空
        /// </summary>
        /// <returns>如果类型是引用类型或可空类型，则返回 true</returns>
        /// <see>
        ///     <cref>http://stackoverflow.com/a/1770232</cref>
        /// </see>
        public static bool IsNullable(this Type type)
        {
            return type.IsReferenceType() || Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// 检查类型是否为引用类型
        /// </summary>
        /// <returns>如果类型不是值类型，则返回 true</returns>
        public static bool IsReferenceType(this Type type)
        {
            return !type.IsValueType;
        }

        /// <summary>
        /// 检查类型是否为 struct
        /// </summary>
        /// <returns>如果类型是值类型，但不是原始类型或枚举类型，则返回 true</returns>
        public static bool IsStruct(this Type type)
        {
            return type.IsValueType && !type.IsPrimitive && !type.IsEnum;
        }

        /// <summary>
        /// 检查类型是否可以从给定对象转换而来
        /// </summary>
        public static bool IsAssignableFrom<T>(this Type type, in T value)
        {
            // 如果对象为空，则检查类型是否可空
            if (value == null) return type.IsNullable();
            // 否则，使用类型实例化对象实例并检查类型是否与该实例匹配
            return type.IsInstanceOfType(value);
        }

        /// <summary>
        /// 检查类型是否为基本类型
        /// </summary>
        public static bool IsBasic(this Type type)
        {
            // 如果类型是 string 或 decimal 类型，则返回 true
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (type == typeof(string) || type == typeof(decimal)) return true;
            // 如果类型是枚举类型或原始类型，则返回 true
            if (type.IsEnum) return true;
            if (type.IsPrimitive)
            {
                if (type == typeof(IntPtr) || type == typeof(UIntPtr)) return false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 检查类型是否为静态
        /// </summary>
        /// <returns>如果类型是抽象的和密封的，则返回 true（静态类）</returns>
        public static bool IsStatic(this Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }

        /// <summary>
        /// 检查类型是否为抽象
        /// </summary>
        /// <returns>如果类型是抽象的，则返回 true，但不要用于静态类型</returns>
        public static bool IsAbstract(this Type type)
        {
            return type.IsAbstract && !type.IsSealed;
        }

        /// <summary>
        /// 检查类型是否具体
        /// </summary>
        /// <returns>如果类型不是抽象的、接口或包含泛型参数，则返回 true</returns>
        public static bool IsConcrete(this Type type)
        {
            return !type.IsAbstract && !type.IsInterface && !type.ContainsGenericParameters;
        }

        /// <summary>
        /// 判断一个类型是否能通过另一个类型构造而来
        /// </summary>
        public static bool IsMakeGenericTypeVia(this Type openConstructedType, Type closedConstructedType)
        {
            if (openConstructedType is null) throw new ArgumentNullException(nameof(openConstructedType));
            if (closedConstructedType is null) throw new ArgumentNullException(nameof(closedConstructedType));

            if (openConstructedType == closedConstructedType) return true;
            if (openConstructedType.IsGenericParameter) // 如果开放式构造类型是泛型参数（例如：T）
            {
                var constraintAttributes = openConstructedType.GenericParameterAttributes;
                if (constraintAttributes != GenericParameterAttributes.None)
                {
                    if (constraintAttributes.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint) &&
                        !closedConstructedType.IsValueType)
                        return false;

                    if (constraintAttributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint) &&
                        closedConstructedType.IsValueType)
                        return false;

                    if (constraintAttributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint) &&
                        closedConstructedType.GetConstructor(Type.EmptyTypes) == null)
                        return false;
                }

                return openConstructedType.GetGenericParameterConstraints()
                    .All(constraint => constraint.IsAssignableFrom(closedConstructedType));
            }

            while (openConstructedType != null && openConstructedType.ContainsGenericParameters)
            {
                if (closedConstructedType == null) return false;

                if (openConstructedType.IsGenericType &&
                    closedConstructedType.IsGenericType &&
                    openConstructedType.GetGenericTypeDefinition() == closedConstructedType.GetGenericTypeDefinition())
                {
                    var openArgs = openConstructedType.GetGenericArguments();
                    var closedArgs = closedConstructedType.GetGenericArguments();

                    return !openArgs.Where((t, i) => !IsMakeGenericTypeVia(t, closedArgs[i])).Any();
                }

                if (openConstructedType.IsArray)
                {
                    if (!closedConstructedType.IsArray || closedConstructedType.GetArrayRank() != openConstructedType.GetArrayRank())
                        return false;

                    openConstructedType = openConstructedType.GetElementType();
                    closedConstructedType = closedConstructedType.GetElementType();
                }
                else if (openConstructedType.IsByRef)
                {
                    if (!closedConstructedType.IsByRef) return false;
                    openConstructedType = openConstructedType.GetElementType();
                    closedConstructedType = closedConstructedType.GetElementType();
                }
                else
                {
                    return openConstructedType.IsAssignableFrom(closedConstructedType);
                }
            }

            if (openConstructedType == null) return false;
            return openConstructedType.IsAssignableFrom(closedConstructedType);
        }

        /// <summary>
        /// 使用另一个关闭构造类型的类型参数，将开放式构造类型解析为已关闭构造类型。
        /// 请务必小心谨慎，并且确保所有的泛型参数和类型都正确地指定和解析，以避免出现意外行为和错误结果。
        /// </summary>
        /// <param name="openConstructedType">要解析的开放式构造类型。</param>
        /// <param name="closedConstructedType">将用于解析的已关闭构造类型。</param>
        /// <param name="resolvedGenericParameters">递归解析过程中已解析的泛型参数的字典。此参数可以为空，但不应为 null。</param>
        /// <param name="safe">如果为 true，则在无法安全地将开放式构造类型解析为已关闭构造类型时引发异常。如果为 false，则回退到不安全的行为，这可能会导致错误的结果或异常。</param>
        /// <returns>使用已关闭构造类型的类型参数实例化时与开放式构造类型对应的已关闭构造类型。</returns>
        public static Type MakeGenericTypeVia(
            this Type openConstructedType,
            in Type closedConstructedType,
            in IDictionary<Type, Type> resolvedGenericParameters,
            in bool safe = true)
        {
            if (openConstructedType is null) throw new ArgumentNullException(nameof(openConstructedType));
            if (closedConstructedType is null) throw new ArgumentNullException(nameof(closedConstructedType));
            if (resolvedGenericParameters is null) throw new ArgumentNullException(nameof(resolvedGenericParameters));

            if (safe && !openConstructedType.IsMakeGenericTypeVia(closedConstructedType))
                throw new GenericClosingException(openConstructedType, closedConstructedType);

            if (openConstructedType == closedConstructedType)
                return openConstructedType;

            if (openConstructedType.IsGenericParameter)
            {
                // The open-constructed type is a generic parameter.
                // We can directly map it to the closed-constructed type.

                if (!closedConstructedType.ContainsGenericParameters &&
                    !resolvedGenericParameters.TrySet(openConstructedType, closedConstructedType) &&
                    resolvedGenericParameters[openConstructedType] != closedConstructedType)
                    throw new InvalidOperationException("Nested generic parameters resolve to different values.");

                return closedConstructedType;
            }

            if (!openConstructedType.ContainsGenericParameters)
                return openConstructedType;

            if (openConstructedType.IsGenericType)
            {
                var openConstructedGenericDefinition = openConstructedType.GetGenericTypeDefinition();
                var openConstructedGenericArguments = openConstructedType.GetGenericArguments();

                foreach (var inheritedCloseConstructedType in closedConstructedType.AndBaseTypeAndInterfaces())
                {
                    if (inheritedCloseConstructedType.IsGenericType &&
                        inheritedCloseConstructedType.GetGenericTypeDefinition() == openConstructedGenericDefinition)
                    {
                        var inheritedClosedConstructedGenericArguments = inheritedCloseConstructedType.GetGenericArguments();

                        var closedConstructedGenericArguments = new Type[openConstructedGenericArguments.Length];

                        for (var i = 0; i < openConstructedGenericArguments.Length; i++)
                        {
                            closedConstructedGenericArguments[i] = openConstructedGenericArguments[i].MakeGenericTypeVia(
                                inheritedClosedConstructedGenericArguments[i],
                                resolvedGenericParameters,
                                safe: false);
                        }

                        return openConstructedGenericDefinition.MakeGenericType(closedConstructedGenericArguments);
                    }
                }

                throw new GenericClosingException(openConstructedType, closedConstructedType);
            }

            if (openConstructedType.IsArray)
            {
                if (!closedConstructedType.IsArray || closedConstructedType.GetArrayRank() != openConstructedType.GetArrayRank())
                    throw new GenericClosingException(openConstructedType, closedConstructedType);

                var openConstructedElementType = openConstructedType.GetElementType();
                var closedConstructedElementType = closedConstructedType.GetElementType();

                return openConstructedElementType.MakeGenericTypeVia(closedConstructedElementType, resolvedGenericParameters, safe: false)
                    .MakeArrayType(openConstructedType.GetArrayRank());
            }

            if (openConstructedType.IsByRef)
            {
                if (!closedConstructedType.IsByRef)
                    throw new GenericClosingException(openConstructedType, closedConstructedType);

                var openConstructedElementType = openConstructedType.GetElementType();
                var closedConstructedElementType = closedConstructedType.GetElementType();

                return openConstructedElementType.MakeGenericTypeVia(closedConstructedElementType, resolvedGenericParameters, safe: false).MakeByRefType();
            }

            throw new NotImplementedException();
        }
    }
}