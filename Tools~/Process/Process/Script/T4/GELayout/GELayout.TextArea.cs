#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "TextArea", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetTextArea()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("string", "value", "value") { Comments = "文本内容" };
            var paramsList = new FunctionParam[][]
            {
                new FunctionParam[] { value, Options, },
                new FunctionParam[] { value, Style, Options, },
            };
            foreach (var param in paramsList)
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.NewStatic,
                    Comments = "绘制 文本域",
                    Name = "AreaText",
                    Params = param,
                    ReturnType = value.Type,
                };
                chunk.Content = $"return EditorGUILayout.TextArea({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            return chunks;
        }
    }
}