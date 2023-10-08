#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System.Collections.Generic;

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Button", IsArray = true)]
        private static List<FunctionChunk> Button()
        {
            var chunks = new List<FunctionChunk>();
            var action = new FunctionParam("Action", "action", "") { Comments = "回调" };
       
            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            foreach (var item in new string[] { "GUIContent", "string" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { label, Options, },
                    new FunctionParam[] { label, width_float, },
                    new FunctionParam[] { label, width_float, height_float },
                    new FunctionParam[] { label, Style, Options, },
                    new FunctionParam[] { label, Style, width_float, },
                    new FunctionParam[] { label, Style, width_float, height_float },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = "绘制 按钮",
                        Name = "Button",
                        Params = param,
                        ReturnType = "bool",
                    };
                    chunk.Content = $"return GUILayout.Button({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }

                paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { label, action, Options, },
                    new FunctionParam[] { label, action, width_float, },
                    new FunctionParam[] { label, action, width_float, height_float },
                    new FunctionParam[] { label, action, Style, Options, },
                    new FunctionParam[] { label, action, Style, width_float, },
                    new FunctionParam[] { label, action, Style, width_float, height_float },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = "绘制 按钮",
                        Name = "Button",
                        Params = param,
                        ReturnType = "void",
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

            var width_float = new FunctionParam("float", "width", "GUILayout.Width(width)") { Comments = "宽度" };
            var height_float = new FunctionParam("float", "height", "GUILayout.Height(height)") { Comments = "高度" };
            foreach (var item in new string[] { "GUIContent", "string", })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { label, Options, },
                    new FunctionParam[] { label, width_float, },
                    new FunctionParam[] { label, width_float, height_float },
                    new FunctionParam[] { label, Style, Options, },
                    new FunctionParam[] { label, Style, width_float, },
                    new FunctionParam[] { label, Style, width_float, height_float },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = "绘制 按钮",
                        Name = "ButtonRepeat",
                        Params = param,
                        ReturnType = "bool",
                    };
                    chunk.Content = $"return GUILayout.RepeatButton({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            var action = new FunctionParam("Action", "action", "") { Comments = "回调" };
            foreach (var item in new string[] { "GUIContent", "string", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { label, action, Options, },
                    new FunctionParam[] { label, action, width_float, },
                    new FunctionParam[] { label, action, width_float, height_float },
                    new FunctionParam[] { label, action, Style, Options, },
                    new FunctionParam[] { label, action, Style, width_float, },
                    new FunctionParam[] { label, action, Style, width_float, height_float },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = "绘制 按钮",
                        Name = "ButtonRepeat",
                        Params = param,
                        ReturnType = "void",
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
            foreach (var item in new string[] { "GUIContent", "string", "Texture", "GTContent", })
            {
                var label = new FunctionParam(item, "label", "label, FocusType.Passive") { Comments = "标签" };
                if (item != "GUIContent") label.Output = "new GUIContent(label), FocusType.Passive";
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { label, Options, },
                    new FunctionParam[] { label, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 下拉按钮",
                        Name = "ButtonDropdown",
                        Params = param,
                        ReturnType = "bool",
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
            foreach (var item in new string[] { "GUIContent", "string", "Texture", "GTContent", })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { label, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 Link按钮",
                        Name = "ButtonLink",
                        Params = param,
                        ReturnType = "bool",
                        MacroDefinition = "UNITY_2021_1_OR_NEWER",
                    };
                    chunk.Content = $"return EditorGUILayout.LinkButton({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}