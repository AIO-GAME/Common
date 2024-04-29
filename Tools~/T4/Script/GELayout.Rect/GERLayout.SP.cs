#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GERLayoutSingleton
    {
        [FuncParam(Group = "PropertyField", IsArray = true)]
        private static IEnumerable<FunctionChunk> Property()
        {
            var property = new FunctionParam("SerializedProperty", "property", "property") { Comments        = "属性" };
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments                                  = "绘制区域" };
            var pos = new FunctionParam("Vector2", "pos", "new Rect(pos - size / 2, size)") { Comments       = "位置" };
            var size = new FunctionParam("Vector2", "size", "") { Comments                                   = "大小" };
            var includeChildren = new FunctionParam("bool", "includeChildren", "includeChildren") { Comments = "是否包含子属性", Default = "false" };
            var chunks = new List<FunctionChunk>();
            foreach (var label in new[]
            {
                new FunctionParam("string", "label", "new GUIContent(label)") { Comments = "标题" },
                new FunctionParam("GUIContent", "label", "label") { Comments             = "标题" }
            })
            {
                var paramsList = new[]
                {
                    new[] { rect, property, includeChildren },
                    new[] { rect, property, label, includeChildren },
                    new[] { pos, size, property, label, includeChildren }
                };
                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State      = TChunkState.Static,
                        Comments   = "绘制 Serialized Property",
                        Name       = "SP",
                        Params     = param,
                        ReturnType = "bool"
                    };
                    chunk.Content = $"return EditorGUI.PropertyField({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }


            return chunks;
        }
    }
}