#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

#region

using System.Collections.Generic;

#endregion

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "HelpBox", IsArray = true)]
        private static IEnumerable<FunctionChunk> EditorGUILayoutHelpBox()
        {
            var chunks = new List<FunctionChunk>();

            var type = new FunctionParam("MessageType", "type", "type") { Comments = "消息类型", Default = "MessageType.None" };
            var wide = new FunctionParam("bool", "wide", "wide")
            {
                Comments = "true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分", Default = "true"
            };
            foreach (var item in new[] { "string", "GUIContent", "Texture" })
            {
                var message = new FunctionParam(item, "message", "message") { Comments = "消息" };
                var paramsList = new List<FunctionParam[]>();
                switch (item)
                {
                    case "GUIContent":
                        paramsList.Add(new[] { message, wide });
                        break;
                    case "string":
                        paramsList.Add(new[] { message, type, wide });
                        break;
                    case "Texture":
                        message.Output = "new GUIContent(message)";
                        paramsList.Add(new[] { message, wide });
                        break;
                }

                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State    = TChunkState.NewStatic,
                        Comments = "绘制 帮助框 字段",
                        Name     = "HelpBox",
                        Params   = param
                    };
                    chunk.Content = $"EditorGUILayout.HelpBox({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }

        [FuncParam(Group = "HelpBox", IsArray = true)]
        private static IEnumerable<FunctionChunk> EditorGUIHelpBox()
        {
            var chunks = new List<FunctionChunk>();
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments                            = "矩形" };
            var type = new FunctionParam("MessageType", "type", "type") { Comments                     = "消息类型", Default = "MessageType.None" };
            var pos = new FunctionParam("Vector2", "pos", "new Rect(pos - size / 2, size)") { Comments = "位置信息" };
            var size = new FunctionParam("Vector2", "size", "") { Comments                             = "大小信息" };
            foreach (var item in new[] { "string" })
            {
                var message = new FunctionParam(item, "message", "message") { Comments = "消息" };
                var paramsList = new List<FunctionParam[]>
                {
                    new[] { rect, message, type },
                    new[] { pos, size, message, type }
                };

                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State    = TChunkState.NewStatic,
                        Comments = "绘制 帮助框 字段",
                        Name     = "HelpBox",
                        Params   = param
                    };
                    chunk.Content = $"EditorGUI.HelpBox({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}