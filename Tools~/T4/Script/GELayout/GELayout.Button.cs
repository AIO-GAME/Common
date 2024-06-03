#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Button", IsArray = true)]
        private static List<FunctionChunk> Button()
        {
            var chunks = new List<FunctionChunk>();
            var action = new FunctionParam("Action", "action", "") { Comments = "回调" };

            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments     = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            foreach (var item in new[] { "GUIContent", "string" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { label, Options },
                    new[] { label, width_float },
                    new[] { label, width_float, height_float },
                    new[] { label, Style, Options },
                    new[] { label, Style, width_float },
                    new[] { label, Style, width_float, height_float }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 按钮",
                        Name       = "Button",
                        Params     = param,
                        ReturnType = "bool"
                    };
                    chunk.Content = $"return GUILayout.Button({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }

                paramsList = new List<FunctionParam[]>
                {
                    new[] { label, action, Options },
                    new[] { label, action, width_float },
                    new[] { label, action, width_float, height_float },
                    new[] { label, action, Style, Options },
                    new[] { label, action, Style, width_float },
                    new[] { label, action, Style, width_float, height_float }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 按钮",
                        Name       = "Button",
                        Params     = param,
                        ReturnType = "void"
                    };
                    chunk.Content = $"if (GUILayout.Button({chunk.GetParamValues()})) action();";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Button Repeat", IsArray = true)]
        private static List<FunctionChunk> ButtonRepeat()
        {
            var chunks = new List<FunctionChunk>();

            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments     = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            foreach (var item in new[] { "GUIContent", "string" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { label, Options },
                    new[] { label, width_float },
                    new[] { label, width_float, height_float },
                    new[] { label, Style, Options },
                    new[] { label, Style, width_float },
                    new[] { label, Style, width_float, height_float }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 按钮",
                        Name       = "ButtonRepeat",
                        Params     = param,
                        ReturnType = "bool"
                    };
                    chunk.Content = $"return GUILayout.RepeatButton({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            var action = new FunctionParam("Action", "action", "") { Comments = "回调" };
            foreach (var item in new[] { "GUIContent", "string", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { label, action, Options },
                    new[] { label, action, width_float },
                    new[] { label, action, width_float, height_float },
                    new[] { label, action, Style, Options },
                    new[] { label, action, Style, width_float },
                    new[] { label, action, Style, width_float, height_float }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 按钮",
                        Name       = "ButtonRepeat",
                        Params     = param,
                        ReturnType = "void"
                    };
                    chunk.Content = $"if (GUILayout.RepeatButton({chunk.GetParamValues()})) action();";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Button", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetButtonDropdown()
        {
            var chunks = new List<FunctionChunk>();
            foreach (var item in new[] { "GUIContent", "string", "Texture", "GTContent" })
            {
                var label = new FunctionParam(item, "label", "label, FocusType.Passive") { Comments = "标签" };
                if (item != "GUIContent") label.Output = "new GUIContent(label), FocusType.Passive";
                var paramsList = new[]
                {
                    new[] { label, Options },
                    new[] { label, Style, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 下拉按钮",
                        Name       = "ButtonDropdown",
                        Params     = param,
                        ReturnType = "bool"
                    };
                    chunk.Content = $"return EditorGUILayout.DropdownButton({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Button", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetButtonLink()
        {
            var chunks = new List<FunctionChunk>();
            foreach (var item in new[] { "GUIContent", "string", "Texture", "GTContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { label, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State           = TChunkState.Static,
                        Comments        = "绘制 Link按钮",
                        Name            = "ButtonLink",
                        Params          = param,
                        ReturnType      = "bool",
                        MacroDefinition = "UNITY_2021_1_OR_NEWER"
                    };
                    chunk.Content = $"return EditorGUILayout.LinkButton({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Button", IsArray = true)]
        private static List<FunctionChunk> ButtonCopy()
        {
            var chunks = new List<FunctionChunk>();

            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments     = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            var label = new FunctionParam("string", "label", "label") { Comments                           = "标签" };
            var GUIContentLabel = new FunctionParam("GUIContent", "label", "label") { Comments             = "标签" };
            foreach (var content in new[]
            {
                new FunctionParam("T", "content", ""),
                new FunctionParam("string", "content", ""),
                new FunctionParam("long", "content", ""),
                new FunctionParam("double", "content", ""),
                new FunctionParam("Color", "content", ""),
                new FunctionParam("Color32", "content", "")
            })
            {
                content.Comments = "复制值";
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { label, content },
                    new[] { label, content, Style },
                    new[] { label, content, width_float },
                    new[] { label, content, width_float, Style },
                    new[] { label, content, width_float, height_float },
                    new[] { label, content, width_float, height_float, Style },
                    new[] { GUIContentLabel, content },
                    new FunctionParam[] { GUIContentLabel, content, Style },
                    new FunctionParam[] { GUIContentLabel, content, width_float },
                    new FunctionParam[] { GUIContentLabel, content, width_float, Style },
                    new FunctionParam[] { GUIContentLabel, content, width_float, height_float },
                    new FunctionParam[] { GUIContentLabel, content, width_float, height_float, Style }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 复制按钮",
                        Name       = "ButtonCopy",
                        Params     = param,
                        ReturnType = "void"
                    };

                    if (content.Type == "T") chunk.Generics = new Dictionary<string, string> { { "T", "" } };
                    chunk.Content = $"if (GUILayout.Button({chunk.GetParamValues()})) GEHelper.CopyAction(content);";
                    if (chunk.Content.Contains("style"))
                    {
                        chunk.Content = chunk.Content.Replace(", style", "");
                        chunk.Content = chunk.Content.Replace("label,", "label, style,");
                    }

                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Button", IsArray = true)]
        private static List<FunctionChunk> ButtonPaste()
        {
            var chunks = new List<FunctionChunk>();

            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments     = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            var label = new FunctionParam("string", "label", "label") { Comments                           = "标签" };
            foreach (var content in new[]
            {
                new FunctionParam("T", "content", ""),
                new FunctionParam("string", "content", ""),
                new FunctionParam("long", "content", ""),
                new FunctionParam("double", "content", ""),
                new FunctionParam("Color", "content", ""),
                new FunctionParam("Color32", "content", "")
            })
            {
                content.Comments = "复制值";
                content.Modifier = ParamModifier.Ref;
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { label, content },
                    new[] { label, content, Style },
                    new[] { label, content, width_float },
                    new[] { label, content, width_float, Style },
                    new[] { label, content, width_float, height_float },
                    new[] { label, content, width_float, height_float, Style }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 粘贴按钮",
                        Name       = "ButtonPaste",
                        Params     = param,
                        ReturnType = "void"
                    };

                    if (content.Type == "T") chunk.Generics = new Dictionary<string, string> { { "T", "" } };
                    chunk.Content = $"if (GUILayout.Button({chunk.GetParamValues()})) content = GEHelper.PasteAction();";
                    if (chunk.Content.Contains("style"))
                    {
                        chunk.Content = chunk.Content.Replace(", style", "");
                        chunk.Content = chunk.Content.Replace("label,", "label, style,");
                    }

                    chunk.Content = chunk.Content.Replace("GEHelper.PasteAction()", content.Type == "T"
                                                              ? "GEHelper.PasteAction<T>()"
                                                              : $"GEHelper.PasteAction{content.Type[0].ToString().ToUpper()}{content.Type.Substring(1)}()");

                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Button", IsArray = true)]
        private static List<FunctionChunk> ButtonClear()
        {
            var chunks = new List<FunctionChunk>();
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments     = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            var label = new FunctionParam("string", "label", "label") { Comments                           = "标签" };
            var content = new FunctionParam("ICollection<T>", "content", "") { Comments                    = "值" };
            var paramsList = new List<FunctionParam[]>
            {
                new[] { label, content },
                new[] { label, content, Style },
                new[] { label, content, width_float },
                new[] { label, content, width_float, Style },
                new[] { label, content, width_float, height_float },
                new[] { label, content, width_float, height_float, Style }
            };
            foreach (var chunk in paramsList.Select(param => new FunctionChunk
            {
                State      = TChunkState.Static,
                Comments   = "绘制 粘贴按钮",
                Name       = "ButtonClear",
                Params     = param,
                ReturnType = "void",
                Generics   = new Dictionary<string, string> { { "T", "" } }
            }))
            {
                chunk.Content = $"if (GUILayout.Button({chunk.GetParamValues()})) content.Clear();";
                if (chunk.Content.Contains("style"))
                {
                    chunk.Content = chunk.Content.Replace(", style", "");
                    chunk.Content = chunk.Content.Replace("label,", "label, style,");
                }

                chunks.Add(chunk);
            }

            return chunks;
        }

        [FuncParam(Group = "Button", IsArray = true)]
        private static List<FunctionChunk> ButtonAdd()
        {
            var chunks = new List<FunctionChunk>();
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments     = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            var label = new FunctionParam("string", "label", "label") { Comments                           = "标签" };
            var func = new FunctionParam("Func<T>", "func", "") { Comments                                 = "添加值回调" };
            var content = new FunctionParam("ICollection<T>", "content", "") { Comments                    = "值" };
            var paramsList = new List<FunctionParam[]>
            {
                new[] { label, content },
                new[] { label, content, Style },
                new[] { label, content, width_float },
                new[] { label, content, width_float, Style },
                new[] { label, content, width_float, height_float },
                new[] { label, content, width_float, height_float, Style }
            };
            foreach (var chunk in paramsList.Select(param => new FunctionChunk
            {
                State      = TChunkState.Static,
                Comments   = "绘制 粘贴按钮",
                Name       = "ButtonAdd",
                Params     = param,
                ReturnType = "void",
                Generics   = new Dictionary<string, string> { { "T", "" } }
            }))
            {
                chunk.Content = $"if (GUILayout.Button({chunk.GetParamValues()})) content.Add(default);";
                if (chunk.Content.Contains("style"))
                {
                    chunk.Content = chunk.Content.Replace(", style", "");
                    chunk.Content = chunk.Content.Replace("label,", "label, style,");
                }

                chunks.Add(chunk);
            }

            paramsList = new List<FunctionParam[]>
            {
                new[] { label, content, func },
                new[] { label, content, func, Style },
                new[] { label, content, func, width_float },
                new[] { label, content, func, width_float, Style },
                new[] { label, content, func, width_float, height_float },
                new[] { label, content, func, width_float, height_float, Style }
            };
            foreach (var chunk in paramsList.Select(param => new FunctionChunk
            {
                State      = TChunkState.Static,
                Comments   = "绘制 粘贴按钮",
                Name       = "ButtonAdd",
                Params     = param,
                ReturnType = "void",
                Generics   = new Dictionary<string, string> { { "T", "" } }
            }))
            {
                chunk.Content = $"if (GUILayout.Button({chunk.GetParamValues()})) content.Add(func());";
                if (chunk.Content.Contains("style"))
                {
                    chunk.Content = chunk.Content.Replace(", style", "");
                    chunk.Content = chunk.Content.Replace("label,", "label, style,");
                }

                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}