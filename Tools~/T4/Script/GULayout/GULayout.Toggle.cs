#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GULayoutSingleton
    {
        [FuncParam(Group = "Toggle", IsArray = true)]
        private static List<FunctionChunk> Toggle()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("bool", "value", "value") { Comments = "值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { label, value, Style, Options },
                    new[] { label, value, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 按钮",
                        Name       = "Toggle",
                        Params     = param,
                        ReturnType = "bool"
                    };
                    chunk.Content = $"return GUILayout.Toggle({chunk.GetParamValues()});".Replace("label, value,",
                                                                                                  "value, label,");
                    if (!chunk.Content.Contains("style"))
                        chunk.Content = chunk.Content.Replace("options", "GUI.skin.toggle, options");
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}