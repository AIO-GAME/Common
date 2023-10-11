#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Label", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetLabelField()
        {
            var chunks = new List<FunctionChunk>();


            foreach (var item in new string[] { "GUIContent", "string", "int", "bool" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "第一个标签" };
                if (item != "GUIContent" && item != "string") label.Output = "label.ToString()";
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { label, Options },
                    new FunctionParam[] { label, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 标签文本框",
                        Name = "Label",
                        Params = param,
                    };
                    chunk.Content = $"EditorGUILayout.LabelField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            foreach (var item in new string[] { "GUIContent", "string", })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "第一个标签" };
                var label2 = new FunctionParam(item, "label2", "label2") { Comments = "向右显示的标签" };
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { label, label2, Options },
                    new FunctionParam[] { label, label2, Style, Options, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 标签文本框",
                        Name = "Label",
                        Params = param,
                    };
                    chunk.Content = $"EditorGUILayout.LabelField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Label", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetLabelPrefix()
        {
            var chunks = new List<FunctionChunk>();
            var followingStyle = new FunctionParam("GUIStyle", "followingStyle", "followingStyle")
                { Comments = "后面的显示风格", };
            var labelStyle = new FunctionParam("GUIStyle", "labelStyle", "labelStyle")
                { Comments = "显示风格", };
            var followingStyle1 = new FunctionParam("[UnityEngine.Internal.DefaultValue(\"\\\"Button\\\"\")] GUIStyle",
                    "followingStyle", "followingStyle")
                { Comments = "后面的显示风格", };

            foreach (var item in new string[] { "GUIContent", "string", "int", "bool" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "第一个标签" };
                if (item != "GUIContent" && item != "string") label.Output = "label.ToString()";
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { label },
                    new FunctionParam[] { label, followingStyle1 },
                    new FunctionParam[] { label, followingStyle, labelStyle, },
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 可选择标签",
                        Name = "LabelPrefix",
                        Params = param,
                    };
                    chunk.Content = $"EditorGUILayout.PrefixLabel({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Label", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetLabelSelectable()
        {
            var chunks = new List<FunctionChunk>();
            foreach (var item in new string[] { "string", "int" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "第一个标签" };
                if (item != "string") label.Output = "label.ToString()";
                var paramsList = new FunctionParam[][]
                {
                    new FunctionParam[] { label, Options, },
                    new FunctionParam[] { label, Style, Options, }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 可选择标签",
                        Name = "LabelSelectable",
                        Params = param,
                    };
                    chunk.Content = $"EditorGUILayout.SelectableLabel({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}