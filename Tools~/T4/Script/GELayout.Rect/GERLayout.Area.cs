#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GERLayoutSingleton
    {
        [FuncParam(Group = "TextArea", IsArray = true)]
        private static IEnumerable<FunctionChunk> AreaText()
        {
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments                                    = "绘制区域" };
            var content = new FunctionParam("string", "content", "content") { Comments                         = "内容" };
            var content1 = new FunctionParam("string", "content", "content, EditorStyles.textArea") { Comments = "内容" };
            var chunks = new List<FunctionChunk>();
            var paramsList = new[]
            {
                new[] { rect, content, Style },
                new[] { rect, content1 }
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State      = TChunkState.NewStatic,
                    Comments   = "绘制 文本域",
                    Name       = "AreaText",
                    Params     = param,
                    ReturnType = "string"
                };
                chunk.Content = $"return EditorGUI.TextArea({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}