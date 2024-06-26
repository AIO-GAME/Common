﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

namespace AIO
{
    public class ClassParam
    {
        public ClassParam()
        {
            Using          = new List<string>();
            Pragma         = new List<string>();
            Generics       = new Dictionary<string, string>();
            FunctionGroups = new Dictionary<string, List<FunctionChunk>>();
            Header         = new StringBuilder();
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; } = "class";

        /// <summary>
        /// 注释
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// 头部描述
        /// </summary>
        public StringBuilder Header { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否为局部类
        /// </summary>
        public bool IsPartial { get; set; } = false;

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; } = string.Empty;

        /// <summary>
        /// 是否存在命名空间
        /// </summary>
        public bool HasNameSpace => !string.IsNullOrEmpty(NameSpace);

        /// <summary>
        /// 泛型类型
        /// </summary>
        public Dictionary<string, string> Generics { get; set; }

        /// <summary>
        /// 可访问性 public private protected internal
        /// </summary>
        public string Accessibility { get; set; } = "public";

        /// <summary>
        /// 函数参数
        /// </summary>
        public Dictionary<string, List<FunctionChunk>> FunctionGroups { get; set; }

        /// <summary>
        /// 引用命名空间
        /// </summary>
        public List<string> Using { get; set; }

        /// <summary>
        /// 编译指示
        /// </summary>
        public List<string> Pragma { get; set; }

        /// <summary>
        /// 编译指示
        /// </summary>
        public TChunkState State { get; set; }

        /// <summary>
        /// 宏定义
        /// </summary>
        public string MacroDefinition { get; set; } = string.Empty;

        public string GetPragma()
        {
            var sb = new StringBuilder();
            if (Pragma.Count != 0) sb.AppendLine();
            foreach (var item in Pragma) sb.AppendLine($"#pragma {item}");
            return sb.ToString().TrimEnd('\n', '\r');
        }

        public string GetUsing()
        {
            var sb = new StringBuilder();
            if (Using.Count != 0) sb.AppendLine();
            foreach (var item in Using) sb.AppendLine($"using {item};");
            return sb.ToString();
        }

        public string GetNamespace()
        {
            var sb = new StringBuilder();
            if (HasNameSpace) sb.AppendLine($"namespace {NameSpace}").Append('{');
            return sb.ToString();
        }

        public string GetTypeComment()
        {
            if (string.IsNullOrEmpty(Comments)) return string.Empty;
            var sb = new StringBuilder();
            var space = HasNameSpace ? "    " : string.Empty;
            sb.AppendLine($"{space}/// <summary>");
            sb.AppendLine($"{space}/// {Comments}");
            sb.AppendLine($"{space}/// </summary>");
            return sb.ToString().TrimEnd('\n', '\r');
        }

        public string GetTypeHeader()
        {
            var sb = new StringBuilder();
            var space = HasNameSpace ? "    " : string.Empty;
            var accessibility = Accessibility;
            var partial = IsPartial ? " partial" : string.Empty;

            var str = string.Format(
                "{0}{1}{2} {3} {4}"
              , space, accessibility, partial, Type, Name);
            sb.AppendLine(str).Append(space).Append('{');
            return sb.ToString();
        }

        public string GetTypeFooter()
        {
            var sb = new StringBuilder();
            if (HasNameSpace) sb.Append("\n    }");
            sb.Append("\n}");
            return sb.ToString();
        }

        public string GetContent(string def = "", string ext = "")
        {
            var sb = new StringBuilder();
            var space = HasNameSpace ? "        " : "    ";
            var left = string.Concat(space, "{");
            var right = string.Concat(space, "}");
            var headerDic = new Dictionary<string, int>();
            foreach (var group in FunctionGroups)
            {
                sb.AppendLine($"\n{space}#region {group.Key}");
                foreach (var Chunk in group.Value)
                {
                    var header = Chunk.GetHeader(space);
                    if (headerDic.ContainsKey(header)) continue;
                    var attr = Chunk.GetAttribute(space);
                    headerDic.Add(header, 0);
                    var comment = string.Concat("\r\n", Chunk.GetComment(def, ext, space));
                    var hasMD = Chunk.HasMacroDefinition;
                    if (hasMD) sb.AppendLine(string.Concat("\n#if ", Chunk.MacroDefinition));
                    sb.AppendLine(comment);
                    if (!string.IsNullOrEmpty(attr)) sb.AppendLine(attr);
                    sb.AppendLine(header).AppendLine(left);
                    foreach (var line in Chunk.Content.Split('\n'))
                    {
                        if (string.IsNullOrEmpty(line)) continue;
                        sb.AppendLine((line[0] == '#' ? line : string.Concat(space, "    ", line)).TrimEnd('\n', '\r'));
                    }

                    sb.AppendLine(right);

                    if (hasMD) sb.AppendLine(string.Concat("\n#endif"));
                }

                sb.AppendLine($"\n{space}#endregion");
            }

            return sb.ToString().TrimEnd('\n', '\r');
        }

        public void Save(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dir)) return;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.WriteAllText(path, GetAllText(), new UTF8Encoding(false));
        }

        public string GetAllText()
        {
            var sb = new StringBuilder();
            if (Header.Length != 0) sb.AppendLine(Header.ToString().TrimEnd('\n', '\r'));
            sb.AppendLine(GetPragma());
            sb.AppendLine(GetUsing());
            sb.AppendLine(GetNamespace());
            sb.AppendLine(GetTypeComment());
            sb.AppendLine(GetTypeHeader());
            sb.AppendLine(GetContent());
            sb.AppendLine(GetTypeFooter());
            return sb.ToString();
        }
    }
}