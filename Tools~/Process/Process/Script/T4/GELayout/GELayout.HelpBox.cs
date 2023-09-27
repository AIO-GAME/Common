#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GELayoutSingleton
    {
        [FuncParam(Group = "HelpBox", IsArray = true)]
        private static IEnumerable<FunctionChunk> EditorGUILayoutHelpBox()
        {
            var chunks = new List<FunctionChunk>();

            var type = new FunctionParam("MessageType", "type = MessageType.None", "type") { Comments = "消息类型" };
            var wide = new FunctionParam("bool", "wide = true", "wide")
            {
                Comments = "true:帮助框覆盖整个窗口宽度;false:只覆盖控制部分"
            };
            foreach (var item in new string[] { "string", "GUIContent", "Texture" })
            {
                var message = new FunctionParam(item, "message", "message") { Comments = "消息" };
                var paramsList = new List<FunctionParam[]>();
                switch (item)
                {
                    case "GUIContent":
                        paramsList.Add(new FunctionParam[] { message, wide });
                        break;
                    case "string":
                        paramsList.Add(new FunctionParam[] { message, type, wide });
                        break;
                    case "Texture":
                        message.Output = "new GUIContent(message)";
                        paramsList.Add(new FunctionParam[] { message, wide });
                        break;
                }

                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = $"绘制 帮助框 字段",
                        Name = "HelpBox",
                        Params = param,
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
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments = "矩形" };
            var type = new FunctionParam("MessageType", "type = MessageType.None", "type") { Comments = "消息类型" };
            var pos = new FunctionParam("Vector2", "pos", "new Rect(pos + size / 2, size)")
            {
                Comments = "位置信息"
            };
            var size = new FunctionParam("Vector2", "size", "")
            {
                Comments = "大小信息"
            };
            foreach (var item in new string[] { "string", })
            {
                var message = new FunctionParam(item, "message", "message") { Comments = "消息" };
                var paramsList = new List<FunctionParam[]>
                {
                    new FunctionParam[] { rect, message, type },
                    new FunctionParam[] { pos, size, message, type },
                };

                foreach (var param in paramsList)
                {
                    var chunk = new FunctionChunk
                    {
                        State = TChunkState.NewStatic,
                        Comments = "绘制 帮助框 字段",
                        Name = "HelpBox",
                        Params = param,
                    };
                    chunk.Content = $"EditorGUI.HelpBox({chunk.GetParamValues()});";
                    chunks.Add(chunk);
                }
            }

            return chunks;
        }
    }
}