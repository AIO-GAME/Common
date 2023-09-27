#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO
{
    public class FunctionChunk
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get => ContentBuilder.ToString().TrimEnd('\n', '\r');
            set
            {
                if (ContentBuilder == null) ContentBuilder = new StringBuilder();
                ContentBuilder.Clear();
                ContentBuilder.AppendLine(value);
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public StringBuilder ContentBuilder { get; set; } = new StringBuilder();

        /// <summary>
        /// 宏定义
        /// </summary>
        public string MacroDefinition { get; set; }

        /// <summary>
        /// 判断是否有宏定义
        /// </summary>
        public bool HasMacroDefinition => !string.IsNullOrEmpty(MacroDefinition);

        /// <summary>
        /// 参数数组
        /// </summary>
        public FunctionParam[] Params { get; set; }

        /// <summary>
        /// 泛型类型
        /// </summary>
        public Dictionary<string, string> Generics { get; set; }

        /// <summary>
        /// 返回类型
        /// </summary>
        public string ReturnType { get; set; } = "void";

        /// <summary>
        /// 是否是异步函数
        /// </summary>
        public bool IsAsync { get; set; } = false;

        /// <summary>
        /// 函数状态
        /// </summary>
        public TChunkState State { get; set; } = TChunkState.None;

        /// <summary>
        /// 是否静态
        /// </summary>
        public bool IsVirtual { get; set; } = false;

        /// <summary>
        /// 是否静态
        /// </summary>
        public bool IsOverride { get; set; } = false;

        /// <summary>
        /// 可访问性 public private protected internal
        /// </summary>
        public string Accessibility { get; set; } = "public";

        /// <summary>
        /// 注释
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 返回注释
        /// </summary>
        public string ReturnComments { get; set; }

        /// <summary>
        /// 函数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性列表
        /// </summary>
        public List<string> Attributes { get; set; } = new List<string>();

        /// <summary>
        /// 函数属性
        /// </summary>
        public string GetAttribute(string space = "")
        {
            var sb = new StringBuilder();
            if (Attributes.Count > 0) sb.AppendFormat("{1}[{0}]", string.Join(", ", Attributes), space);
            return sb.ToString().TrimEnd(' ');
        }

        /// <summary>
        /// 函数参数名
        /// </summary>
        public string GetParamNames(IEnumerable<FunctionParam> value)
        {
            if (value is null) return string.Empty;
            var sb = new StringBuilder();
            foreach (var param in value)
            {
                if (param is null || string.IsNullOrEmpty(param.Name)) continue;
                if (!IsAsync && ReturnType != "Task" && !param.IsParams && param.Modifier != ParamModifier.None)
                {
                    sb.AppendFormat("{0} {1} {2}, ", param.Modifier.ToString().ToLower(), param.Type, param.Name);
                }
                else if (param.IsParams)
                {
                    sb.AppendFormat("params {0}[] {1}, ", param.Type, param.Name);
                }
                else
                {
                    sb.AppendFormat("{0} {1}, ", param.Type, param.Name);
                }
            }

            return sb.ToString().TrimStart(' ').TrimEnd(',', ' ');
        }

        /// <summary>
        /// 函数参数值
        /// </summary>
        public string GetParamValues()
        {
            if (Params is null) return string.Empty;
            var sb = new StringBuilder();
            foreach (var param in Params)
            {
                if (param is null || string.IsNullOrEmpty(param.Output)) continue;
                sb.Append(param.Output + ", ");
            }

            return sb.Length == 0 ? string.Empty : sb.ToString().TrimEnd(',', ' ');
        }

        /// <summary>
        /// 函数参数注释
        /// </summary>
        private string GetParamComment(string space = "")
        {
            if (Params is null) return string.Empty;
            var sb = new StringBuilder();
            foreach (var param in Params)
            {
                if (param is null || string.IsNullOrEmpty(param.Name)) continue;
                var typename = param.Type?.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
                if (string.IsNullOrEmpty(typename) || typename.Contains('[') || typename.Contains('"'))
                    sb.AppendFormat("{2}/// <param name=\"{0}\">{1}</param>\r\n", param.Name, param.Comments, space);
                else
                    sb.AppendFormat("{3}/// <param name=\"{0}\">{2} <see cref=\"{1}\"/></param>\r\n",
                        param.Name, typename, param.Comments, space);
            }

            return sb.Length == 0 ? string.Empty : sb.ToString();
        }

        /// <summary>
        /// 函数注释
        /// </summary>
        public string GetComment(
            string def = "",
            string ext = "",
            string space = "")
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}/// <summary>\r\n", space);
            sb.AppendFormat("{0}/// {1} {2}\r\n", space, string.IsNullOrEmpty(Comments) ? def : Comments, ext);
            sb.AppendFormat("{0}/// </summary>\r\n", space);
            var param = GetParamComment(space);
            if (!string.IsNullOrEmpty(param)) sb.Append(param);
            if (ReturnType != "void")
            {
                var typename = ReturnType.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
                if (string.IsNullOrEmpty(typename) || typename.Contains('[') || typename.Contains('"'))
                    sb.AppendFormat("{0}/// <returns>{1}</returns>\r\n", space, ReturnComments);
                else
                    sb.AppendFormat("{0}/// <returns>{2}<see cref=\"{1}\"/></returns>\r\n", space, typename,
                        ReturnComments);
            }

            return sb.Length == 0 ? string.Empty : sb.ToString().TrimEnd('\r', '\n');
        }

        /// <summary>
        /// 函数头
        /// </summary>
        public string GetHeader(string space = "")
        {
            var sb = new StringBuilder();
            if (Generics is null || Generics.Count == 0)
            {
                sb.AppendFormat("{0}{1} {2} {3}{4} {5}({6})\r\n", space,
                    Accessibility,
                    ClassHelper.GetState(State),
                    IsAsync ? "async " : string.Empty,
                    ReturnType,
                    Name,
                    GetParamNames(Params));
            }
            else
            {
                var generics = new StringBuilder();
                var first = new StringBuilder();
                foreach (var item in Generics)
                {
                    first.AppendFormat(" {0},", item.Key);
                    if (string.IsNullOrEmpty(item.Value)) continue;
                    generics.AppendFormat(" {0} : {1}", item.Key, item.Value);
                }

                if (generics.Length > 0) generics.Insert(0, "where");

                sb.AppendFormat("{0}{1} {2} {8}{3} {4}<{7}>({5}) {6}\r\n", space,
                    Accessibility,
                    ClassHelper.GetState(State),
                    ReturnType,
                    Name,
                    GetParamNames(Params),
                    generics,
                    first.ToString().Trim(' ', ','),
                    IsAsync ? "async " : string.Empty);
            }

            return sb.Length == 0 ? string.Empty : sb.ToString().TrimEnd('\r', '\n');
        }
    }
}