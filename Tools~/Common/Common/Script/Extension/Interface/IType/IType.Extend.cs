namespace AIO
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// 类型扩展
    /// </summary>
    public static partial class ITypeExtend
    {
        /// <summary>
        /// 获取申明的全部构造函数
        /// </summary>
        public static ConstructorInfo[] GetDeclaredConstructors(this _Type type)
        {
            return type.GetConstructors(IReflectExtend.DeclaredFlags & ~BindingFlags.Static); // LUDIQ: 排除静态构造函数
        }

        /// <summary>
        /// 获取申明构造函数
        /// </summary>
        public static ConstructorInfo GetDeclaredConstructor(this _Type type, in Type[] parameters)
        {
            var ctors = GetDeclaredConstructors(type);

            for (var i = 0; i < ctors.Length; ++i)
            {
                var ctor = ctors[i];
                var ctorParams = ctor.GetParameters();

                if (parameters.Length != ctorParams.Length) continue;
                for (var j = 0; j < ctorParams.Length; ++j)
                {
                    if (ctorParams[j].ParameterType != parameters[j])
                    {
                        continue;
                    }
                }

                return ctor;
            }

            return null;
        }

        private static readonly Dictionary<Type, string>
          TypeNames = new Dictionary<Type, string>
          {
                { typeof(object), "object" },
                { typeof(void), "void" },
                { typeof(bool), "bool" },
                { typeof(byte), "byte" },
                { typeof(sbyte), "sbyte" },
                { typeof(char), "char" },
                { typeof(string), "string" },
                { typeof(short), "short" },
                { typeof(int), "int" },
                { typeof(long), "long" },
                { typeof(ushort), "ushort" },
                { typeof(uint), "uint" },
                { typeof(ulong), "ulong" },
                { typeof(float), "float" },
                { typeof(double), "double" },
                { typeof(decimal), "decimal" },
          };

        private static readonly Dictionary<Type, string>
            FullTypeNames = new Dictionary<Type, string>(TypeNames);

        /// <summary>
        /// 获取C#中类型的名称
        /// </summary>
        /// <remarks>获取存储在字典缓存中的值</remarks>
        public static string GetNameCS(this Type type, in bool fullName = true)
        {
            if (type == null) return "null";

            // Check if we have already got the name for that type.
            var names = fullName ? FullTypeNames : TypeNames;

            if (names.TryGetValue(type, out var name))
                return name;

            var text = new System.Text.StringBuilder();

            if (type.IsArray)// Array = TypeName[].
            {
                text.Append(type.GetElementType().GetNameCS(fullName));

                text.Append('[');
                var dimensions = type.GetArrayRank();
                while (dimensions-- > 1)
                    text.Append(',');
                text.Append(']');

                goto Return;
            }

            if (type.IsPointer)// Pointer = TypeName*.
            {
                text.Append(type.GetElementType().GetNameCS(fullName));
                text.Append('*');

                goto Return;
            }

            if (type.IsGenericParameter)// Generic Parameter = TypeName (for unspecified generic parameters).
            {
                text.Append(type.Name);
                goto Return;
            }

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType != null)// Nullable = TypeName != null ?
            {
                text.Append(underlyingType.GetNameCS(fullName));
                text.Append('?');

                goto Return;
            }

            // Other Type = Namespace.NestedTypes.TypeName<GenericArguments>.

            if (fullName && type.Namespace != null)// Namespace.
            {
                text.Append(type.Namespace);
                text.Append('.');
            }

            var genericArguments = 0;

            if (type.DeclaringType != null)// Account for Nested Types.
            {
                // Count the nesting level.
                var nesting = 1;
                var declaringType = type.DeclaringType;
                while (declaringType.DeclaringType != null)
                {
                    declaringType = declaringType.DeclaringType;
                    nesting++;
                }

                // Append the name of each outer type, starting from the outside.
                while (nesting-- > 0)
                {
                    // Walk out to the current nesting level.
                    // This avoids the need to make a list of types in the nest or to insert type names instead of appending them.
                    declaringType = type;
                    for (int i = nesting; i >= 0; i--)
                        declaringType = declaringType.DeclaringType;

                    // Nested Type Name.
                    genericArguments = AppendNameAndGenericArguments(text, declaringType, fullName, genericArguments);
                    text.Append('.');
                }
            }

            // Type Name.
            AppendNameAndGenericArguments(text, type, fullName, genericArguments);

        Return:// Remember and return the name.
            name = text.ToString();
            names.Add(type, name);
            return name;
        }

        /// <summary>
        /// Appends the generic arguments of `type` (after skipping the specified number).
        /// </summary>
        private static int AppendNameAndGenericArguments(StringBuilder text, Type type, in bool fullName = true, int skipGenericArguments = 0)
        {
            var name = type.Name;
            text.Append(name);

            if (type.IsGenericType)
            {
                var backQuote = name.IndexOf('`');
                if (backQuote >= 0)
                {
                    text.Length -= name.Length - backQuote;

                    var genericArguments = type.GetGenericArguments();
                    if (skipGenericArguments < genericArguments.Length)
                    {
                        text.Append('<');

                        var firstArgument = genericArguments[skipGenericArguments];
                        skipGenericArguments++;

                        if (firstArgument.IsGenericParameter)
                        {
                            while (skipGenericArguments < genericArguments.Length)
                            {
                                text.Append(',');
                                skipGenericArguments++;
                            }
                        }
                        else
                        {
                            text.Append(firstArgument.GetNameCS(fullName));

                            while (skipGenericArguments < genericArguments.Length)
                            {
                                text.Append(", ");
                                text.Append(genericArguments[skipGenericArguments].GetNameCS(fullName));
                                skipGenericArguments++;
                            }
                        }

                        text.Append('>');
                    }
                }
            }

            return skipGenericArguments;
        }
    }
}
