#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Mask", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetMaskField()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("int", "mask", "mask") { Comments = "选择值" };

            foreach (var item in new string[] { "string", "GUIContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                foreach (var displayedOption in new FunctionParam[]
                         {
                             new FunctionParam("string[]", "displayedOptions", "displayedOptions"),
                             new FunctionParam("IEnumerable<string>", "displayedOptions", "displayedOptions.ToArray()"),
                         })
                {
                    displayedOption.Comments = "选择内容";
                    var paramsList = new FunctionParam[][]
                    {
                        new FunctionParam[] { value, displayedOption, Options, },
                        new FunctionParam[] { value, displayedOption, Style, Options, },
                        new FunctionParam[] { label, value, displayedOption, Options, },
                        new FunctionParam[] { label, value, displayedOption, Style, Options, },
                    };
                    foreach (var param in paramsList)
                    {
                        var chunk = new FunctionChunk
                        {
                            State = TChunkState.NewStatic,
                            Comments = $"绘制 可选择标签",
                            Name = "Mask",
                            Params = param,
                            ReturnType = "int",
                        };
                        chunk.Content = $"return EditorGUILayout.MaskField({chunk.GetParamValues()});";
                        chunks.Add(chunk);
                    }
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Enum", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetEnumFlagsField()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("T", "selected", "selected") { Comments = "枚举值" };
            var includeObsolete = new FunctionParam("bool", "includeObsolete", "includeObsolete")
            {
                Comments = "true:包含带有ObsoleteAttribute的枚举值,false:排除"
            };
            foreach (var item in new string[] { "GUIContent", "string", "int", "bool" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                switch (item)
                {
                    case "int":
                    case "bool":
                        label.Output = "new GUIContent(label.ToString())";
                        break;
                    case "Texture":
                    case "string":
                        label.Output = "new GUIContent(label)";
                        break;
                }

                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { value, Options, },
                    new FunctionParam[] { label, value, Options, },
                    new FunctionParam[] { label, value, includeObsolete, Options, },
                    new FunctionParam[] { value, Style, Options, },
                    new FunctionParam[] { label, value, Style, Options, },
                    new FunctionParam[] { label, value, includeObsolete, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 枚举菜单",
                        Name = "EnumFlags",
                        Params = param,
                        MacroDefinition = "UNITY_2018_1_OR_NEWER",
                        Generics = new Dictionary<string, string> { { "T", "Enum" } },
                        ReturnType = "T",
                    };
                    chunk.Content = $"return (T)EditorGUILayout.EnumFlagsField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Enum", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetEnumMaskPopup2020_OR_LOWER()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("T", "selected", "selected") { Comments = "枚举值" };
            foreach (var item in new string[] { "GUIContent", "string" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { label, value, Options, },
                    new FunctionParam[] { label, value, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 枚举菜单",
                        Name = "EnumPopupMask",
                        Params = param,
                        MacroDefinition = "!UNITY_2019_1_OR_NEWER",
                        Generics = new Dictionary<string, string> { { "T", "Enum" } },
                        ReturnType = "T",
                    };
                    chunk.Content = $"return (T)EditorGUILayout.EnumMaskPopup({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Enum", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetEnumMaskField2020_OR_LOWER()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("T", "value", "value") { Comments = "枚举值" };
            foreach (var item in new string[] { "GUIContent", "string" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { value, Options, },
                    new FunctionParam[] { value, Style, Options, },
                    new FunctionParam[] { label, value, Options, },
                    new FunctionParam[] { label, value, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 枚举菜单",
                        Name = "EnumMask",
                        Params = param,
                        MacroDefinition = "!UNITY_2020_1_OR_NEWER",
                        Generics = new Dictionary<string, string> { { "T", "Enum" } },
                        ReturnType = "T",
                    };
                    chunk.Content = $"return (T)EditorGUILayout.EnumMaskField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}