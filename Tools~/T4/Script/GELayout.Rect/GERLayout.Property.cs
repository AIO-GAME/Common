#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GERLayoutSingleton
    {
        [FuncParam(Group = "Property Rect", IsArray = true)]
        private static List<FunctionChunk> VPropertyRect()
        {
            var chunks = new List<FunctionChunk>();
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments                           = "矩形" };
            var action = new FunctionParam("Action", "action", "") { Comments                         = "回调函数" };
            var property = new FunctionParam("SerializedProperty", "property", "property") { Comments = "属性" };
            var label = new FunctionParam("GTContent", "label", "label") { Comments                   = "标签" };

            {
                var chunk = new FunctionChunk
                {
                    State      = TChunkState.Static,
                    Comments   = "绘制 属性排版",
                    Name       = "VPropertyRect",
                    Params     = new[] { rect, label, property, action },
                    Attributes = new List<string> { "ExcludeFromDocs" }
                };
                chunk.ContentBuilder.AppendLine($"EditorGUI.BeginProperty({chunk.GetParamValues()});");
                chunk.ContentBuilder.AppendLine("action?.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUI.EndProperty();");
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State      = TChunkState.Static,
                    Comments   = "开始绘制 属性排版",
                    Name       = "VPropertyRectBegin",
                    Params     = new[] { rect, label, property },
                    Attributes = new List<string> { "ExcludeFromDocs" }
                };
                chunk.Content = $"EditorGUI.BeginProperty({chunk.GetParamValues()});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State      = TChunkState.Static,
                    Comments   = "结束绘制 属性排版",
                    Name       = "VPropertyRectEnd",
                    Params     = new FunctionParam[] { },
                    Attributes = new List<string> { "ExcludeFromDocs" },
                    Content    = "EditorGUI.EndProperty();"
                };
                chunks.Add(chunk);
            }
            return chunks;
        }
    }
}