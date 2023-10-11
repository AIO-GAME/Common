#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Popup", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetPopupInt()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("int", "value", "value") { Comments = "值" };

            var optionValues = new FunctionParam[]
            {
                new FunctionParam("int[]", "optionValues", "optionValues") { Comments = "排版格式" },
                new FunctionParam("IEnumerable<int>", "optionValues", "optionValues.ToArray()") { Comments = "排版格式" }
            };

            foreach (var item in new string[] { "string", "GUIContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                var displayedOptions = new List<FunctionParam>()
                {
                    new FunctionParam($"{item}[]", "displayedOptions", "displayedOptions"),
                    new FunctionParam($"IEnumerable<{item}>", "displayedOptions", "displayedOptions.ToArray()"),
                };
                if (item == "string")
                {
                    displayedOptions.Add(new FunctionParam($"int[]", "displayedOptions",
                        "displayedOptions.Select(T => T.ToString()).ToArray()"));
                    displayedOptions.Add(new FunctionParam($"IEnumerable<int>", "displayedOptions",
                        "displayedOptions.Select(T => T.ToString()).ToArray()"));
                }

                foreach (var displayedOption in displayedOptions)
                {
                    displayedOption.Comments = "弹窗内容";
                    foreach (var optionValue in optionValues)
                    {
                        var paramsList = new FunctionParam[][]
                        {
                            new FunctionParam[] { value, displayedOption, optionValue, Options, },
                            new FunctionParam[] { label, value, displayedOption, optionValue, Options, },
                            new FunctionParam[] { value, displayedOption, optionValue, Style, Options, },
                            new FunctionParam[] { label, value, displayedOption, optionValue, Style, Options, },
                        };
                        foreach (var param in paramsList)
                        {
                            var chunk = new FunctionChunk
                            {
                                State = TChunkState.NewStatic,
                                Comments = $"绘制 整数弹窗 字段",
                                Name = "Popup",
                                Params = param,
                                ReturnType = "int",
                            };
                            chunk.Content = $"return EditorGUILayout.IntPopup({chunk.GetParamValues()});";
                            chunks.Add(chunk);
                        }
                    }

                    {
                        var paramsList = new FunctionParam[][]
                        {
                            new FunctionParam[] { value, displayedOption, Options, },
                            new FunctionParam[] { label, value, displayedOption, Options, },
                            new FunctionParam[] { value, displayedOption, Style, Options, },
                            new FunctionParam[] { label, value, displayedOption, Style, Options, },
                        };
                        foreach (var param in paramsList)
                        {
                            var chunk = new FunctionChunk
                            {
                                State = TChunkState.NewStatic,
                                Comments = $"绘制 弹窗 字段",
                                Name = "Popup",
                                Params = param,
                                ReturnType = "int",
                            };
                            chunk.Content = $"return EditorGUILayout.Popup({chunk.GetParamValues()});";
                            chunks.Add(chunk);
                        }
                    }
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Popup", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetPopupEnum_2018_3_OR_NEWER()
        {
            var chunks = new List<FunctionChunk>();
            var checkEnabled = new FunctionParam("Func<Enum, bool>", "checkEnabled", "checkEnabled")
            {
                Comments = "显示每个Enum值,返回指定的方法"
            };
            var includeObsolete = new FunctionParam("bool", "includeObsolete", "includeObsolete")
            {
                Comments = "true:包含带有attribute的枚举值,false:排除"
            };
            var value = new FunctionParam("T", "selected", "selected") { Comments = "枚举值" };
            foreach (var item in new string[] { "string", "GUIContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item != "GUIContent") label.Output = "new GUIContent(label)";
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { value, Options },
                    new FunctionParam[] { value, Style, Options },
                    new FunctionParam[] { label, value, Options },
                    new FunctionParam[] { label, value, Style, Options },
                    new FunctionParam[] { label, value, checkEnabled, includeObsolete, Options },
                    new FunctionParam[] { label, value, checkEnabled, includeObsolete, Style, Options },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        Generics = new Dictionary<string, string> { { "T", "Enum" } },
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 弹窗枚举 字段",
                        Name = "Popup",
                        Params = param,
                        ReturnType = value.Type,
                        MacroDefinition = "UNITY_2018_3_OR_NEWER",
                    };
                    chunk.Content = $"return (T)EditorGUILayout.EnumPopup({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}