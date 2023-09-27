#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GULayoutSingleton
    {
        [FuncParam(Group = "Label", IsArray = true)]
        private static List<FunctionChunk> Label()
        {
            var chunks = new List<FunctionChunk>();
            foreach (var item in new string[] { "string", "GUIContent", "float", })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                else if (item != "string" && item != "GUIContent") label.Output = "new GUIContent(label.ToString())";

                var paramsList = new List<FunctionParam[]>()
                {
                    new FunctionParam[] { label, Options, },
                    new FunctionParam[] { label, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.Static,
                        Comments = "绘制 标签名",
                        Name = "Label",
                        Params = param,
                        ReturnType = "void",
                    };
                    chunk.Content = $"GUILayout.Label({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}