﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Tag", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetTagField()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("string", "value", "value") { Comments = "值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, Options },
                    new[] { label, value, Options },
                    new[] { value, Style, Options },
                    new[] { label, value, Style, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = "绘制 标签字段",
                        Name       = "Tag",
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.TagField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}