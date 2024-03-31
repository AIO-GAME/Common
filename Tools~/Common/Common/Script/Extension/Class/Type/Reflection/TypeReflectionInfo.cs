using System;
using System.Linq;
using System.Reflection;
using System.Text;
using AIO.YamlDotNet;

namespace AIO
{
    /// <summary>
    /// 类型反射信息
    /// </summary>
    public class TypeReflectionInfo : ReflectionInfo
    {
        private Type _info;

        /// <summary>
        /// 空
        /// </summary>
        public static TypeReflectionInfo Empty { get; } = new TypeReflectionInfo();

        /// <summary>
        /// 是否为泛型类型
        /// </summary>
        public bool IsGenericType { get; protected set; }

        /// <summary>
        /// 泛型申明
        /// </summary>
        public string FullGenericArgument
        {
            get
            {
                if (GenericArguments is null || GenericArguments.Length == 0) return string.Empty;
                var str = new StringBuilder();
                for (var i = 0; i < GenericArguments.Length; i++)
                {
                    if (string.IsNullOrEmpty(GenericArguments[i])) continue;
                    str.Append(GenericArguments[i]);
                    if (i != GenericArguments.Length - 1)
                        str.Append(", ");
                }

                return str.ToString();
            }
        }

        /// <summary>
        /// 泛型约束
        /// </summary>
        public string FullGenericConstraint
        {
            get
            {
                if (GenericConstraints is null || GenericConstraints.Length == 0) return string.Empty;
                var str = new StringBuilder();
                for (var i = 0; i < GenericConstraints.Length; i++)
                {
                    str.Append(GenericConstraints[i]);
                    if (i != GenericConstraints.Length - 1) str.Append(", ");
                }

                return str.ToString();
            }
        }

        /// <summary>
        /// 泛型声明
        /// </summary>
        public string[] GenericArguments { get; private set; }

        /// <summary>
        /// 泛型约束
        /// </summary>
        public string[] GenericConstraints { get; private set; }

        /// <summary>
        /// 是否为数组
        /// </summary>
        public bool IsArray { get; protected set; }

        /// <summary>
        /// 是否为指针
        /// </summary>
        public bool IsPointer { get; protected set; }

        private TypeReflectionInfo()
        {
            Name = "type is null";
        }

        /// <inheritdoc /> 
        internal TypeReflectionInfo(Type info)
        {
            if (info is null)
            {
                Name = "type is null";
                return;
            }

            _info = info;
            Access = info.IsPublic ? "public" :
                info.IsNotPublic ? "private" :
                info.IsNestedFamily ? "protected" : "internal";
            IsUnsafe = info.IsSecurityCritical;
            IsStatic = info.IsAbstract && info.IsSealed;
            IsGeneric = info.IsGenericType;
            IsGenericType = info.IsGenericType;
            IsArray = info.IsArray;
            IsPointer = info.IsPointer;
            if (info == typeof(void))
            {
                Name = "void";
                return;
            }

            if (info.IsGenericType) // 判断是否为泛型类型 
            {
                var definition = info.GetGenericTypeDefinition();
                var genericArguments = info.GetGenericArguments();
                var genericTypeStr = definition.FullName;
                if (string.IsNullOrEmpty(genericTypeStr))
                    Name = definition.Name;
                else
                {
                    Name = genericTypeStr.Substring(0, genericTypeStr.IndexOf('`'));
                    GenericArguments = genericArguments.Select(genericArgument => genericArgument.IsGenericParameter
                        ? genericArgument.Name
                        : new TypeReflectionInfo(genericArgument).ToString()).ToArray();
                    if (info.IsGenericTypeDefinition)
                    {
                        var genericConstraints = _info.MakeGenericType(genericArguments).GetGenericArguments();
                        GenericConstraints = new string[genericConstraints.Length];
                        var tempBuilder = new StringBuilder();
                        for (var index = 0; index < genericConstraints.Length; index++)
                        {
                            tempBuilder.Clear();

                            var g = genericConstraints[index];
                            var hasReferenceTypeConstraint = g.GenericParameterAttributes.HasFlag(
                                GenericParameterAttributes.ReferenceTypeConstraint);
                            var hasNotNullableValueTypeConstraint = g.GenericParameterAttributes.HasFlag(
                                GenericParameterAttributes.NotNullableValueTypeConstraint);
                            var hasDefaultConstructorConstraint = g.GenericParameterAttributes.HasFlag(
                                GenericParameterAttributes.DefaultConstructorConstraint);

                            if (hasReferenceTypeConstraint)
                                tempBuilder.Append("class, ");
                            else if (hasNotNullableValueTypeConstraint)
                                tempBuilder.Append("struct, ");

                            tempBuilder.Append(string.Join(",", g.
                                GetGenericParameterConstraints().
                                Select(c => c.ToDetails())));

                            if (hasNotNullableValueTypeConstraint)
                                tempBuilder.Replace("System.ValueType", "");
                            else if (hasDefaultConstructorConstraint)
                                tempBuilder.Append("new()");

                            if (tempBuilder.Length <= 0) continue;

                            GenericConstraints[index] =
                                string.Concat($"where {g.Name} : ", tempBuilder.ToString().Trim(' ', ','));
                        }

                        GenericConstraints = GenericConstraints.Exclude();
                    }
                }
            }
            else if (info.ContainsGenericParameters || info.IsGenericParameter)
            {
                Name = info.Name; // 判断是否为泛型参数
            }
            else if (info.IsArray)
            {
                if (info.ContainsGenericParameters)
                {
                    Name = info.Name;
                }
                else Name = info.GetElementType().ToDetails() + "[]";
            }
            else if (info.IsByRef)
            {
                Name = info.GetElementType().ToDetails() + "&";
            }
            else if (info.IsPointer)
            {
                Name = info.GetElementType().ToDetails() + "*";
            }
            else Name = info.FullName;

            if (string.IsNullOrEmpty(Name)) throw new NotSupportedException("type is null");
            Name = Name.
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
                Replace("&", "").
                Replace('+', '.');
        }

        /// <inheritdoc />
        protected override string FullDescription()
        {
            if (_info is null) return Name;
            var str = new StringBuilder
            {
                Capacity = 32,
                Length = 0
            };
            str.Append(Name);
            var temp = FullGenericArgument;
            if (!string.IsNullOrEmpty(temp)) str.Append('<').Append(temp).Append('>');
            temp = FullGenericConstraint;
            if (!string.IsNullOrEmpty(temp)) str.Append(" ").Append(temp);
            return str.ToString();
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            GenericArguments = null;
            GenericConstraints = null;
            _info = null;
        }
    }
}