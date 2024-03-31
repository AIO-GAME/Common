using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 函数反射信息
    /// </summary>
    public class MethodReflectionInfo : ReflectionInfo
    {
        /// <summary>
        /// 空
        /// </summary>
        public static MethodReflectionInfo Empty { get; } = new MethodReflectionInfo();

        private MethodReflectionInfo() => Name = "methodInfo is null";

        private MethodInfo _info;

        /// <summary>
        /// 泛型申明
        /// </summary>
        public string[] GenericArguments { get; private set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string[] ParameterNames { get; private set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string[] ParameterTypes { get; private set; }

        /// <summary>
        /// 泛型约束
        /// </summary>
        public string[] GenericConstraints { get; private set; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public TypeReflectionInfo ReturnType { get; protected set; }

        /// <summary>
        /// 是否为异步
        /// </summary>
        public bool IsAsync { get; protected set; }


        /// <summary>
        /// 泛型申明
        /// </summary>
        public string FullGenericArgument
        {
            get
            {
                if (GenericArguments.Length == 0) return string.Empty;
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
        /// 参数
        /// </summary>
        public string FullParameter
        {
            get
            {
                if (ParameterTypes.Length == 0) return string.Empty;
                var str = new StringBuilder();
                for (var i = 0; i < ParameterTypes.Length; i++)
                {
                    if (string.IsNullOrEmpty(ParameterTypes[i])) continue;
                    str.Append(ParameterTypes[i]).Append(' ').Append(ParameterNames[i]);
                    if (i != ParameterTypes.Length - 1)
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
                if (GenericConstraints.Length == 0) return string.Empty;
                var str = new StringBuilder();
                for (var i = 0; i < GenericConstraints.Length; i++)
                {
                    if (string.IsNullOrEmpty(GenericConstraints[i])) continue;
                    str.Append(GenericConstraints[i]);
                    if (i != GenericConstraints.Length - 1) str.Append(", ");
                }

                return str.ToString();
            }
        }

        /// <inheritdoc />
        internal MethodReflectionInfo(MethodInfo info)
        {
            if (info is null)
            {
                Name = "methodInfo is null";
                return;
            }

            _info = info;
            Name = info.Name;
            Access =
                info.IsPublic ? "public" :
                info.IsPrivate ? "private" :
                info.IsFamily ? "protected" : "internal";
            IsAsync = info.IsOpAsync();
            IsUnsafe = info.IsOpUnsafe();
            IsStatic = info.IsStatic;
            IsGeneric = info.IsGenericMethod;
            ReturnType = new TypeReflectionInfo(info.ReturnType);

            var parameters = info.GetParameters();
            ParameterNames = new string[parameters.Length];
            ParameterTypes = new string[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                using var parameter = new ParameterInfoReflectionInfo(parameters[i]);
                ParameterNames[i] = parameter.Name;
                ParameterTypes[i] = $"{parameter.Attributes}{parameter.Type}";
            }

            if (_info.IsGenericMethod)
            {
                var genericArguments = _info.GetGenericArguments();
                GenericArguments = new string[genericArguments.Length];
                for (var index = 0; index < genericArguments.Length; index++)
                {
                    var argument = genericArguments[index];
                    if (argument is null) continue;
                    GenericArguments[index] = argument.Name;
                }

                var genericConstraints = info.MakeGenericMethod(genericArguments).GetGenericArguments();
                GenericConstraints = new string[genericConstraints.Length];
                var tempBuilder = new StringBuilder();
                for (var index = 0; index < genericConstraints.Length; index++)
                {
                    tempBuilder.Clear();

                    var g = genericConstraints[index];
                    var hasReferenceTypeConstraint =
                        g.GenericParameterAttributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint);
                    var hasNotNullableValueTypeConstraint =
                        g.GenericParameterAttributes.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint);
                    var hasDefaultConstructorConstraint =
                        g.GenericParameterAttributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint);

                    if (hasReferenceTypeConstraint) tempBuilder.Append("class, ");
                    else if (hasNotNullableValueTypeConstraint) tempBuilder.Append("struct, ");

                    tempBuilder.Append(string.Join(",", g.
                        GetGenericParameterConstraints().
                        Select(c => c.ToDetails())));

                    if (hasNotNullableValueTypeConstraint) tempBuilder.Replace("System.ValueType", "");
                    else if (hasDefaultConstructorConstraint) tempBuilder.Append("new()");

                    if (tempBuilder.Length <= 0) continue;

                    GenericConstraints[index] =
                        string.Concat($"where {g.Name} : ", tempBuilder.ToString().Trim(' ', ','));
                }

                GenericConstraints = GenericConstraints.Exclude();
                GenericArguments = GenericArguments.Exclude();
            }
            else
            {
                GenericConstraints = Array.Empty<string>();
                GenericArguments = Array.Empty<string>();
            }

            ParameterNames = ParameterNames.Exclude();
            ParameterTypes = ParameterTypes.Exclude();
        }

        /// <inheritdoc />
        protected override string FullDescription()
        {
            if (_info is null) return Name;
            var str = new StringBuilder
            {
                Capacity = 64,
                Length = 0
            };
            str.Append(_info.ReflectedType.ToDetails()).Append(' ');
            str.Append(Access).Append(' ');
            str.Append(IsUnsafe ? "unsafe " : string.Empty);
            str.Append(IsStatic ? "static " : " ");
            str.Append(IsAsync ? "async " : string.Empty);
            str.Append(ReturnType.Name).Append(' ');
            str.Append(Name);

            if (GenericArguments.Length > 0) str.Append('<').Append(string.Join(", ", GenericArguments)).Append('>');
            str.Append('(');
            if (ParameterTypes.Length > 0)
            {
                for (var index = 0; index < ParameterTypes.Length; index++)
                {
                    str.Append(ParameterTypes[index]);
                    str.Append(' ').Append(ParameterNames[index]);
                    if (index != ParameterTypes.Length - 1) str.Append(", ");
                }
            }

            str.Append(')');
            if (GenericConstraints.Length > 0) str.Append(' ').Append(string.Join(" ", GenericConstraints));
            return str.ToString();
        }


        /// <inheritdoc />
        public override void Dispose()
        {
            ParameterTypes = null;
            ParameterNames = null;
            GenericArguments = null;
            GenericConstraints = null;
            ReturnType?.Dispose();
            ReturnType = null;
            _info = null;
        }
    }
}