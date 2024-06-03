#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GULayoutSingleton
    {
        [FuncParam(Group = "Button", IsArray = true)]
        private static List<FunctionChunk> Button()
        {
            var chunks = new List<FunctionChunk>();
            var action = new FunctionParam("Action", "action", "") { Comments                              = "回调" };
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments                                = "绘制区域" };
            var pos = new FunctionParam("Vector2", "pos", "new Rect(pos - size / 2, size)") { Comments     = "位置" };
            var size = new FunctionParam("Vector2", "size", "") { Comments                                 = "大小" };
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

            foreach (var item in new[] { "GUIContent", "string", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { rect, label },
                    new[] { rect, label, Style },
                    new[] { pos, size, label },
                    new[] { pos, size, label, Style }
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
                    chunk.Content = $"return GUI.Button({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }

                paramsList = new List<FunctionParam[]>
                {
                    new[] { rect, label, action },
                    new[] { rect, label, action, Style },
                    new[] { pos, size, label, action },
                    new[] { pos, size, label, action, Style }
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
                    chunk.ContentBuilder.AppendLine("if (action is null) return;");
                    chunk.ContentBuilder.AppendLine($"if (GUI.Button({chunk.GetParamValues()})) action();");
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
    }
}