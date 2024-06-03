#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "Slider", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetSliderInt()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("int", "value", "value") { Comments                = "值" };
            var leftValue = new FunctionParam("int", "leftValue", "leftValue") { Comments    = "左侧值" };
            var rightValue = new FunctionParam("int", "rightValue", "rightValue") { Comments = "右侧值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, leftValue, rightValue, Options },
                    new[] { label, value, leftValue, rightValue, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = "绘制 滑动条",
                        Name       = "Slider",
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.IntSlider({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Slider", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetSlider()
        {
            var chunks = new List<FunctionChunk>();
            var value = new FunctionParam("float", "value", "value") { Comments                = "值" };
            var leftValue = new FunctionParam("float", "leftValue", "leftValue") { Comments    = "左侧值" };
            var rightValue = new FunctionParam("float", "rightValue", "rightValue") { Comments = "右侧值" };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                if (item == "Texture") label.Output = "new GUIContent(label)";
                var paramsList = new[]
                {
                    new[] { value, leftValue, rightValue, Options },
                    new[] { label, value, leftValue, rightValue, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.NewStatic,
                        Comments   = "绘制 滑动条",
                        Name       = "Slider",
                        Params     = param,
                        ReturnType = value.Type
                    };
                    chunk.Content = $"return EditorGUILayout.Slider({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "Slider", IsArray = true)]
        private static IEnumerable<FunctionChunk> GetMinMaxSlider()
        {
            var chunks = new List<FunctionChunk>();
            var minValue = new FunctionParam("float", "minValue", "ref minValue")
            {
                Comments = "滑动条最左边的值",
                Modifier = ParamModifier.Ref
            };
            var maxValue = new FunctionParam("float", "maxValue", "ref maxValue")
            {
                Comments = "滑动条最右边的值",
                Modifier = ParamModifier.Ref
            };
            var minLimit = new FunctionParam("float", "minLimit", "minLimit") { Comments = "限制滑动条最左边的值" };
            var maxLimit = new FunctionParam("float", "maxLimit", "maxLimit") { Comments = "限制滑动条最右边的值" };
            foreach (var item in new[] { "string", "GUIContent" })
            {
                var label = new FunctionParam(item, "label", "label") { Comments = "标签" };
                var paramsList = new[]
                {
                    new[] { minValue, maxValue, minLimit, maxLimit, Options },
                    new[] { label, minValue, maxValue, minLimit, maxLimit, Options }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State    = TChunkState.NewStatic,
                        Comments = "绘制 限制滑动条",
                        Name     = "Slider",
                        Params   = param
                    };
                    chunk.Content = $"EditorGUILayout.MinMaxSlider({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}