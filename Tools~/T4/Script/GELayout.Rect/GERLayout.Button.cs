#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GERLayoutSingleton
    {
        [FuncParam(Group = "Button", IsArray = true)]
        private static List<FunctionChunk> Button()
        {
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments                            = "绘制区域" };
            var pos = new FunctionParam("Vector2", "pos", "new Rect(pos - size / 2, size)") { Comments = "位置" };
            var size = new FunctionParam("Vector2", "size", "") { Comments                             = "大小" };
            var action = new FunctionParam("Action", "action", "") { Comments                          = "回调" };
            var chunks = new List<FunctionChunk>();

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
                        State      = TChunkState.NewStatic,
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
                        State      = TChunkState.NewStatic,
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
    }
}