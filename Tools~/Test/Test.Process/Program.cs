using AIO;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AIO
{
    public static class Testss
    {
        /// <summary>
        /// 获取类型及其基类和接口实现的列表
        /// </summary>
        public static IEnumerable<Type> AndBaseTypeAndInterfaces(this Type type)
        {
            return type.Yield().Concat(type.BaseTypeAndInterfaces());
        }

        public static IEnumerable<Type> BaseTypeAndInterfaces(this Type type, bool inheritedInterfaces = true)
        {
            var types = Enumerable.Empty<Type>();

            if (type.BaseType != null)
            {
                types = types.Concat(type.BaseType.Yield());
            }

            types = types.Concat(type.GetInterfaces(inheritedInterfaces));

            return types;
        }

        /// <summary>
        /// 获取接口
        /// </summary>
        public static IEnumerable<Type> GetInterfaces(this Type type, in bool includeInherited)
        {
            if (includeInherited || type.BaseType == null)
            {
                return type.GetInterfaces();
            }

            return type.GetInterfaces().Except(type.BaseType.GetInterfaces());
        }

        public static IEnumerable<T> Yield<T>(this T t)
        {
            yield return t;
        }

        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        /// <typeparam name="K">任意泛型</typeparam>
        /// <typeparam name="V">任意泛型</typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TrySet<K, V>(this IDictionary<K, V> dic, in K key, in V value)
        {
            if (dic is null) throw new ArgumentNullException(nameof(dic));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (dic.ContainsKey(key))
            {
                dic[key] = value;
                return true;
            }

            return false;
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

                foreach (var constraint in openConstructedType.GetGenericParameterConstraints())
                {
                    if (!constraint.IsAssignableFrom(closedConstructedType)) return false;
                }

                return true;
            }

            while (openConstructedType.ContainsGenericParameters)
            {
                if (openConstructedType.IsGenericType &&
                    closedConstructedType.IsGenericType &&
                    openConstructedType.GetGenericTypeDefinition() == closedConstructedType.GetGenericTypeDefinition())
                {
                    var openArgs = openConstructedType.GetGenericArguments();
                    var closedArgs = closedConstructedType.GetGenericArguments();

                    for (var i = 0; i < openArgs.Length; i++)
                    {
                        if (!IsMakeGenericTypeVia(openArgs[i], closedArgs[i]))
                            return false;
                    }

                    return true;
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

            return openConstructedType.IsAssignableFrom(closedConstructedType);
        }

        /// <summary>>
        /// 使用另一个关闭构造类型的类型参数，将开放式构造类型解析为已关闭构造类型。
        /// </summary>
        /// <param name="openConstructedType">要解析的开放式构造类型。</param>
        /// <param name="closedConstructedType">将用于解析的已关闭构造类型。</param>
        /// <param name="resolvedGenericParameters">递归解析过程中已解析的泛型参数的字典。此参数可以为空，但不应为 null。</param>
        /// <param name="safe">如果为 true，则在无法安全地将开放式构造类型解析为已关闭构造类型时引发异常。如果为 false，则回退到不安全的行为，这可能会导致错误的结果或异常。</param>
        /// <returns>使用已关闭构造类型的类型参数实例化时与开放式构造类型对应的已关闭构造类型。</returns>
        public static Type MakeGenericTypeVia(
            this Type openConstructedType,
            Type closedConstructedType,
            IDictionary<Type, Type> resolvedGenericParameters,
            bool safe = true)
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

                return openConstructedElementType.MakeGenericTypeVia(closedConstructedElementType, resolvedGenericParameters, safe: false).MakeArrayType(openConstructedType.GetArrayRank());
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

    /// <summary>
    /// 这是一个密封类 GenericClosingException，继承自 Exception
    /// </summary>
    public sealed class GenericClosingException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GenericClosingException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public GenericClosingException(Type open, Type closed) : base($"Open-constructed type '{open}' is not assignable from closed-constructed type '{closed}'.")
        {
        }
    }

    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(" ------------ Start Program ------------ ");
            Test();
            Console.WriteLine(" ------------ End Program ------------ ");
            Console.Read();
        }

        public static void MakeGenericTypeVia_Should_Resolve_Closed_Generic_Type()
        {
            // Arrange
            var openConstructedType = typeof(List<>);
            var closedConstructedType = typeof(List<int>);
            var expectedClosedConstructedType = typeof(List<int>);

            var resolvedGenericParameters = new Dictionary<Type, Type>();

            // Act
            var actualClosedConstructedType = openConstructedType.MakeGenericTypeVia(closedConstructedType, resolvedGenericParameters);

            // Assert
            // Assert.AreEqual(expectedClosedConstructedType, actualClosedConstructedType);
        }

        public static void MakeGenericTypeVia_Should_Resolve_Nested_Generic_Type()
        {
            // Arrange
            var openConstructedType = typeof(Dictionary<,>);
            var closedConstructedType = typeof(Dictionary<string, List<int>>);
            var expectedClosedConstructedType = typeof(Dictionary<string, List<int>>);

            var resolvedGenericParameters = new Dictionary<Type, Type>();

            // Act
            var actualClosedConstructedType = openConstructedType.MakeGenericTypeVia(closedConstructedType, resolvedGenericParameters);

            // Assert
            // Assert.AreEqual(expectedClosedConstructedType, actualClosedConstructedType);
        }

        private static void Test()
        {
            MakeGenericTypeVia_Should_Resolve_Nested_Generic_Type();
        }
    }
}