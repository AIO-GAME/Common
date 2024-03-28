using System;
using System.Reflection;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 参数反射信息
    /// </summary>
    public class ParameterInfoReflectionInfo : ReflectionInfo
    {
        /// <summary>
        /// 空
        /// </summary>
        public static ParameterInfoReflectionInfo Empty { get; } = new ParameterInfoReflectionInfo();

        private ParameterInfo _info;

        /// <summary>
        /// 参数类型
        /// </summary>
        private TypeReflectionInfo ParameterType { get; set; }

        public bool IsIn => _info.IsIn;

        public bool IsOut => _info.IsOut;

        public bool IsRef => _info.ParameterType.IsByRef;

        public string DefaultValue => _info.DefaultValue?.ToString();

        public string Position => _info.Position.ToString();

        public string Attributes { get; private set; }

        public string Type { get; private set; }

        private ParameterInfoReflectionInfo()
        {
            Name = "parameter is null";
        }

        /// <inheritdoc />
        internal ParameterInfoReflectionInfo(ParameterInfo info)
        {
            _info = info;
            Name = info.Name;
            ParameterType = new TypeReflectionInfo(info.ParameterType);
            Access = ParameterType.Access;
            IsUnsafe = ParameterType.IsUnsafe;
            IsStatic = false;
            IsGeneric = ParameterType.IsGeneric;
            if (_info.IsIn)
                Attributes = "in ";
            else if (_info.IsOut)
                Attributes = "out ";
            else if (_info.ParameterType.IsByRef)
                Attributes = "ref ";
            else if (_info.GetCustomAttribute<ParamArrayAttribute>() != null)
                Attributes = "params ";

            // 判断类型是否为泛型参数

            if (info.ParameterType.IsGenericType)
            {
                Type = info.ParameterType.ToDetails();
            }
            else if (info.ParameterType.ContainsGenericParameters ||
                     info.ParameterType.IsGenericParameter)
            {
                var genericArguments = info.ParameterType.GetGenericArguments();
                if (genericArguments.Length > 0)
                {
                    var temp = new StringBuilder();
                    for (var i = 0; i < genericArguments.Length; i++)
                    {
                        temp.Append(genericArguments[i].Name);
                        if (i != genericArguments.Length - 1)
                            temp.Append(", ");
                    }

                    Type = info.ParameterType.Name.Replace($"`{genericArguments.Length}", $"<{temp}>").Replace("&", "");
                }
                else Type = info.ParameterType.Name.Replace("&", "");
            }
            else
            {
                Type = info.ParameterType.ToDetails().ToString().Replace("&", "");
            }
        }

        /// <inheritdoc />
        protected override string FullDescription()
        {
            return $"{Attributes}{Type} {Name}";
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _info = null;
        }
    }
}