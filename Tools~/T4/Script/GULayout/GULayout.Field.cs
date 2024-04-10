#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GULayoutSingleton
    {
        [FuncParam(Group = "Field Text", IsArray = true)]
        private static List<FunctionChunk> TextField()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("string", "text", "text") { Comments            = "文本内容" };
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
                    Comments   = "绘制 文本框",
                    Name       = "Field",
                    Params     = param,
                    ReturnType = "string"
                };
                chunk.Content = $"return GUILayout.TextField({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}