#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Toggle", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetRightToggle()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("bool", "value", "value") { Comments = "值" };
            foreach (var item in new string[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { value, Options, },
                    new FunctionParam[] { label, value, Options, },
                    new FunctionParam[] { value, Style, Options, },
                    new FunctionParam[] { label, value, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 左侧按钮",
                        Name = "Toggle",
                        Params = param,
                        ReturnType = value.Type,
                    };
                    chunk.Content = $"return EditorGUILayout.Toggle({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }

                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 左侧按钮",
                        Name = "Field",
                        Params = param,
                        ReturnType = value.Type,
                    };
                    chunk.Content = $"return EditorGUILayout.Toggle({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Toggle", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetLeftToggle()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("bool", "value", "value") { Comments = "值" };
            foreach (var item in new string[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { label, value, Options, },
                    new FunctionParam[] { label, value, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 右侧按钮",
                        Name = "ToggleLeft",
                        Params = param,
                        ReturnType = value.Type,
                    };
                    chunk.Content = $"return EditorGUILayout.ToggleLeft({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}