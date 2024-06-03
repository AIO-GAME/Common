#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GULayoutSingleton
    {
        [FuncParam(Group = "Area Text", IsArray = true)]
        private static List<FunctionChunk> TextArea()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("string", "text", "text") { Comments            = "文本内容" };
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments               = "绘制区域" };
            var maxLength = new FunctionParam("int", "maxLength", "maxLength") { Comments = "输入字符串最大长度" };
            var paramsList = new List<FunctionParam[]>
            {
                new[] { value, Options },
                new[] { value, Style, Options },
                new[] { value, maxLength, Options },
                new[] { value, maxLength, Style, Options }
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State      = TChunkState.Static,
                    Comments   = "绘制 文本视图",
                    Name       = "AreaText",
                    Params     = param,
                    ReturnType = "string"
                };
                chunk.Content = $"return GUILayout.TextArea({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            paramsList = new List<FunctionParam[]>
            {
                new[] { rect, value },
                new[] { rect, value, Style },
                new[] { rect, value, maxLength },
                new[] { rect, value, maxLength, Style }
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State      = TChunkState.Static,
                    Comments   = "绘制 文本视图",
                    Name       = "AreaText",
                    Params     = param,
                    ReturnType = "string"
                };
                chunk.Content = $"return GUI.TextArea({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}