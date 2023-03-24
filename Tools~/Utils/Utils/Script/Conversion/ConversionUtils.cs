using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AIO
{
    /// <summary>
    /// 转换工具类
    /// </summary>
    public static class ConversionUtils
    {
        private const BindingFlags UserDefinedBindingFlags = BindingFlags.Static | BindingFlags.Public;

        private static readonly Dictionary<ConversionQuery, ConversionType> conversionTypesCache = new Dictionary<ConversionQuery, ConversionType>(new ConversionQueryComparer());

        private static readonly Dictionary<ConversionQuery, MethodInfo[]> userConversionMethodsCache = new Dictionary<ConversionQuery, MethodInfo[]>(new ConversionQueryComparer());

        private static bool RespectsIdentity(in Type source, in Type destination)
        {
            return source == destination;
        }

        private static bool ExpectsString(in Type source, in Type destination)
        {
            return destination == typeof(string);
        }

        /// <summary>
        /// 是否存在 隐式数字转换
        /// </summary>
        public static bool HasImplicitNumericConversion(in Type source, in Type destination)
        {
            return implicitNumericConversions.ContainsKey(source) && implicitNumericConversions[source].Contains(destination);
        }

        /// <summary>
        /// 是否存在 显式数字转换
        /// </summary>
        public static bool HasExplicitNumericConversion(in Type source, in Type destination)
        {
            return explicitNumericConversions.ContainsKey(source) && explicitNumericConversions[source].Contains(destination);
        }

        /// <summary>
        /// 是否存在 数字转换功能
        /// </summary>
        public static bool HasNumericConversion(in Type source, in Type destination)
        {
            return HasImplicitNumericConversion(source, destination) || HasExplicitNumericConversion(source, destination);
        }

        /// <summary>
        /// 查找用户定义的转换方法
        /// </summary>
        private static IEnumerable<MethodInfo> FindUserDefinedConversionMethods(in ConversionQuery query)
        {
            var source = query.source;
            var destination = query.destination;

            var sourceMethods = source.GetMethods(UserDefinedBindingFlags)
                .Where(m => m.IsUserDefinedConversion());

            var destinationMethods = destination.GetMethods(UserDefinedBindingFlags)
                .Where(m => m.IsUserDefinedConversion());

            return sourceMethods.Concat(destinationMethods).Where
            (
                m => m.GetParameters()[0].ParameterType.IsAssignableFrom(source) ||
                     source.IsAssignableFrom(m.GetParameters()[0].ParameterType)
            );
        }

        // Returning an array directly so that the enumeration in
        // UserDefinedConversion does not allocate memory
        private static MethodInfo[] GetUserDefinedConversionMethods(in Type source, in Type destination)
        {
            var query = new ConversionQuery(source, destination);

            if (!userConversionMethodsCache.ContainsKey(query))
            {
                userConversionMethodsCache.Add(query, FindUserDefinedConversionMethods(query).ToArray());
            }

            return userConversionMethodsCache[query];
        }

        private static ConversionType GetUserDefinedConversionType(in Type source, Type destination)
        {
            var conversionMethods = GetUserDefinedConversionMethods(source, destination);

            // Duplicate user defined conversions are not allowed, so FirstOrDefault is safe.

            // Look for direct conversions.
            var conversionMethod = conversionMethods.FirstOrDefault(m => m.ReturnType == destination);

            if (conversionMethod != null)
            {
                switch (conversionMethod.Name)
                {
                    case "op_Implicit":
                        return ConversionType.UserDefinedImplicit;
                    case "op_Explicit":
                        return ConversionType.UserDefinedExplicit;
                }
            }
            // Primitive types can skip the middleman cast, even if it is explicit.
            else if (destination.IsPrimitive && destination != typeof(IntPtr) && destination != typeof(UIntPtr))
            {
                // Look for implicit conversions.
                conversionMethod = conversionMethods.FirstOrDefault(m => HasImplicitNumericConversion(m.ReturnType, destination));

                if (conversionMethod != null)
                {
                    switch (conversionMethod.Name)
                    {
                        case "op_Implicit":
                            return ConversionType.UserDefinedThenNumericImplicit;
                        case "op_Explicit":
                            return ConversionType.UserDefinedThenNumericExplicit;
                    }
                }
                // Look for explicit conversions.
                else
                {
                    conversionMethod = conversionMethods.FirstOrDefault(m => HasExplicitNumericConversion(m.ReturnType, destination));

                    if (conversionMethod != null)
                    {
                        return ConversionType.UserDefinedThenNumericExplicit;
                    }
                }
            }

            return ConversionType.Impossible;
        }

        private static bool HasEnumerableToArrayConversion(in Type source, in Type destination)
        {
            return source != typeof(string) &&
                   typeof(IEnumerable).IsAssignableFrom(source) &&
                   destination.IsArray &&
                   destination.GetArrayRank() == 1;
        }

        private static bool HasEnumerableToListConversion(in Type source, in Type destination)
        {
            return source != typeof(string) &&
                   typeof(IEnumerable).IsAssignableFrom(source) &&
                   destination.IsGenericType &&
                   destination.GetGenericTypeDefinition() == typeof(List<>);
        }

        private static bool IsValidConversion(in ConversionType conversionType, in bool guaranteed)
        {
            if (conversionType == ConversionType.Impossible)
            {
                return false;
            }

            if (guaranteed)
            {
                // Downcasts are not guaranteed to succeed.
                if (conversionType == ConversionType.Downcast)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 是否能转换
        /// </summary>
        public static bool CanConvert(in object value, in Type type, in bool guaranteed)
        {
            return IsValidConversion(GetRequiredConversion(value, type), guaranteed);
        }

        /// <summary>
        /// 是否能转换
        /// </summary>
        public static bool CanConvert(in Type source, in Type destination, in bool guaranteed)
        {
            return IsValidConversion(GetRequiredConversion(source, destination), guaranteed);
        }

        /// <summary>
        /// 转换
        /// </summary>
        public static object Convert(in object value, in Type type)
        {
            return Convert(value, type, GetRequiredConversion(value, type));
        }

        /// <summary>
        /// 转换
        /// </summary>
        public static T Convert<T>(in object value)
        {
            return (T)Convert(value, typeof(T));
        }

        /// <summary>
        /// 尝试转换
        /// </summary>
        public static bool TryConvert(in object value, in Type type, out object result, in bool guaranteed)
        {
            var conversionType = GetRequiredConversion(value, type);

            if (IsValidConversion(conversionType, guaranteed))
            {
                result = Convert(value, type, conversionType);
                return true;
            }

            result = value;
            return false;
        }

        /// <summary>
        /// 尝试转换
        /// </summary>
        public static bool TryConvert<T>(in object value, out T result, in bool guaranteed)
        {
            if (TryConvert(value, typeof(T), out var res, guaranteed))
            {
                result = (T)res;
                return true;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// 可转化为
        /// </summary>
        public static bool IsConvertibleTo(this Type source, in Type destination, in bool guaranteed)
        {
            return CanConvert(source, destination, guaranteed);
        }

        /// <summary>
        /// 可转化为
        /// </summary>
        public static bool IsConvertibleTo(this object source, in Type type, in bool guaranteed)
        {
            return CanConvert(source, type, guaranteed);
        }

        /// <summary>
        /// 可转化为
        /// </summary>
        public static bool IsConvertibleTo<T>(this object source, in bool guaranteed)
        {
            return CanConvert(source, typeof(T), guaranteed);
        }

        /// <summary>
        /// 转化为
        /// </summary>
        public static object ConvertTo(this object source, in Type type)
        {
            return Convert(source, type);
        }

        /// <summary>
        /// 转化为
        /// </summary>
        public static T ConvertTo<T>(this object source)
        {
            return (T)Convert(source, typeof(T));
        }

        /// <summary>
        /// 获得所需的转换
        /// </summary>
        public static ConversionType GetRequiredConversion(in Type source, in Type destination)
        {
            var query = new ConversionQuery(source, destination);

            if (!conversionTypesCache.TryGetValue(query, out var conversionType))
            {
                conversionType = DetermineConversionType(query);
                conversionTypesCache.Add(query, conversionType);
            }

            return conversionType;
        }

        private static ConversionType DetermineConversionType(in ConversionQuery query)
        {
            var source = query.source;
            var destination = query.destination;

            if (source == null)
            {
                if (destination.IsNullable())
                {
                    return ConversionType.Identity;
                }
                else
                {
                    return ConversionType.Impossible;
                }
            }

            Ensure.That(nameof(destination)).IsNotNull(destination);

            if (RespectsIdentity(source, destination))
            {
                return ConversionType.Identity;
            }

            if (source.IsUpcast(destination))
            {
                return ConversionType.Upcast;
            }

            if (source.IsDowncast(destination))
            {
                return ConversionType.Downcast;
            }

            // Disabling *.ToString conversion, because it's more often than otherwise very confusing
            /*else if (ExpectsString(source, destination))
            {
                return ConversionType.ToString;
            }*/

            if (HasImplicitNumericConversion(source, destination))
            {
                return ConversionType.NumericImplicit;
            }

            if (HasExplicitNumericConversion(source, destination))
            {
                return ConversionType.NumericExplicit;
            }

            if (HasEnumerableToArrayConversion(source, destination))
            {
                return ConversionType.EnumerableToArray;
            }

            if (HasEnumerableToListConversion(source, destination))
            {
                return ConversionType.EnumerableToList;
            }

            {
                var userDefinedConversionType = GetUserDefinedConversionType(source, destination);
                if (userDefinedConversionType != ConversionType.Impossible)
                {
                    return userDefinedConversionType;
                }
            }

            return ConversionType.Impossible;
        }

        /// <summary>
        /// 
        /// </summary>
        public static ConversionType GetRequiredConversion(in object value, in Type type)
        {
            Ensure.That(nameof(type)).IsNotNull(type);
            return GetRequiredConversion(value?.GetType(), type);
        }

        private static object NumericConversion(in object value, in Type type)
        {
            return System.Convert.ChangeType(value, type);
        }

        private static object UserDefinedConversion(in ConversionType conversion, in object value, in Type type)
        {
            var valueType = value.GetType();
            var conversionMethods = GetUserDefinedConversionMethods(valueType, type);

            var numeric = conversion == ConversionType.UserDefinedThenNumericImplicit ||
                          conversion == ConversionType.UserDefinedThenNumericExplicit;

            MethodInfo conversionMethod = null;

            if (numeric)
            {
                foreach (var m in conversionMethods)
                {
                    if (HasNumericConversion(m.ReturnType, type))
                    {
                        conversionMethod = m;
                        break;
                    }
                }
            }
            else
            {
                foreach (var m in conversionMethods)
                {
                    if (m.ReturnType == type)
                    {
                        conversionMethod = m;
                        break;
                    }
                }
            }

            var result = conversionMethod.InvokeOptimized(null, value);

            if (numeric)
            {
                result = NumericConversion(result, type);
            }

            return result;
        }

        private static object Convert(in object value, in Type type, in ConversionType conversionType)
        {
            Ensure.That(nameof(type)).IsNotNull(type);

            if (conversionType == ConversionType.Impossible)
            {
                throw new InvalidConversionException($"Cannot convert from '{value?.GetType().ToString() ?? "null"}' to '{type}'.");
            }

            try
            {
                switch (conversionType)
                {
                    case ConversionType.Identity:
                    case ConversionType.Upcast:
                    case ConversionType.Downcast:
                        return value;

                    case ConversionType.ToString:
                        return value.ToString();

                    case ConversionType.NumericImplicit:
                    case ConversionType.NumericExplicit:
                        return NumericConversion(value, type);

                    case ConversionType.UserDefinedImplicit:
                    case ConversionType.UserDefinedExplicit:
                    case ConversionType.UserDefinedThenNumericImplicit:
                    case ConversionType.UserDefinedThenNumericExplicit:
                        return UserDefinedConversion(conversionType, value, type);

                    default:
                        throw new UnexpectedEnumValueException<ConversionType>(conversionType);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidConversionException($"Failed to convert from '{value?.GetType().ToString() ?? "null"}' to '{type}' via {conversionType}.", ex);
            }
        }

        private readonly struct ConversionQuery : IEquatable<ConversionQuery>
        {
            public readonly Type source;
            public readonly Type destination;

            public ConversionQuery(Type source, Type destination)
            {
                this.source = source;
                this.destination = destination;
            }

            public bool Equals(ConversionQuery other)
            {
                return
                    source == other.source &&
                    destination == other.destination;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is ConversionQuery))
                {
                    return false;
                }

                return Equals((ConversionQuery)obj);
            }

            public override int GetHashCode()
            {
                return HashUtils.GetHashCode(source, destination);
            }
        }

        // Make sure the equality comparer doesn't use boxing
        private struct ConversionQueryComparer : IEqualityComparer<ConversionQuery>
        {
            public bool Equals(ConversionQuery x, ConversionQuery y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(ConversionQuery obj)
            {
                return obj.GetHashCode();
            }
        }

        #region Numeric Conversions

        // https://msdn.microsoft.com/en-us/library/y5b434w4.aspx
        private static readonly Dictionary<Type, HashSet<Type>> implicitNumericConversions = new Dictionary<Type, HashSet<Type>>()
        {
            {
                typeof(sbyte),
                new HashSet<Type>()
                {
                    typeof(byte),
                    typeof(int),
                    typeof(long),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                }
            },
            {
                typeof(byte),
                new HashSet<Type>()
                {
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                }
            },
            {
                typeof(short),
                new HashSet<Type>()
                {
                    typeof(int),
                    typeof(long),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                }
            },
            {
                typeof(ushort),
                new HashSet<Type>()
                {
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(float),
                    typeof(double),
                    typeof(decimal),
                }
            },
            {
                typeof(int),
                new HashSet<Type>()
                {
                    typeof(long),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                }
            },
            {
                typeof(uint),
                new HashSet<Type>()
                {
                    typeof(long),
                    typeof(ulong),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                }
            },
            {
                typeof(long),
                new HashSet<Type>()
                {
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                }
            },
            {
                typeof(char),
                new HashSet<Type>()
                {
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                }
            },
            {
                typeof(float),
                new HashSet<Type>()
                {
                    typeof(double)
                }
            },
            {
                typeof(ulong),
                new HashSet<Type>()
                {
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                }
            },
        };

        // https://msdn.microsoft.com/en-us/library/yht2cx7b.aspx
        private static readonly Dictionary<Type, HashSet<Type>> explicitNumericConversions = new Dictionary<Type, HashSet<Type>>()
        {
            {
                typeof(sbyte),
                new HashSet<Type>()
                {
                    typeof(byte),
                    typeof(ushort),
                    typeof(uint),
                    typeof(ulong),
                    typeof(char)
                }
            },
            {
                typeof(byte),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(char)
                }
            },
            {
                typeof(short),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(ushort),
                    typeof(uint),
                    typeof(ulong),
                    typeof(char)
                }
            },
            {
                typeof(ushort),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short),
                    typeof(char)
                }
            },
            {
                typeof(int),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short),
                    typeof(ushort),
                    typeof(uint),
                    typeof(ulong),
                    typeof(char)
                }
            },
            {
                typeof(uint),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(char)
                }
            },
            {
                typeof(long),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(ulong),
                    typeof(char)
                }
            },
            {
                typeof(ulong),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(char)
                }
            },
            {
                typeof(char),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short)
                }
            },
            {
                typeof(float),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(char),
                    typeof(decimal)
                }
            },
            {
                typeof(double),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(char),
                    typeof(float),
                    typeof(decimal),
                }
            },
            {
                typeof(decimal),
                new HashSet<Type>()
                {
                    typeof(sbyte),
                    typeof(byte),
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(char),
                    typeof(float),
                    typeof(double)
                }
            }
        };

        #endregion
    }
}