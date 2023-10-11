#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GERLayoutSingleton
    {
        [FuncParam(Group = "Disabled Group", IsArray = true)]
        private static List<FunctionChunk> VDisabledGroup()
        {
            var chunks = new List<FunctionChunk>();
            var action = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var disable = new FunctionParam("bool", "disable", "disable") { Comments = "禁用" };
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "开启代码块来检查GUI更改",
                    Name = "VDisabledGroup",
                    Params = new FunctionParam[] { action, disable },
                    Attributes = new List<string> { "ExcludeFromDocs" },
                };
                chunk.ContentBuilder.AppendLine("if (action == null) return;");
                chunk.ContentBuilder.AppendLine("EditorGUI.BeginDisabledGroup(disable);");
                chunk.ContentBuilder.AppendLine("action.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUI.EndChangeCheck();");
                chunks.Add(chunk);
            }

            chunks.Add(new FunctionChunk
            {
                State = TChunkState.Static,
                Comments = "启动一个新的代码块来检查GUI更改",
                Name = "VDisabledGroupBegin",
                Params = new FunctionParam[] { disable },
                Attributes = new List<string> { "ExcludeFromDocs" },
                Content = "EditorGUI.BeginDisabledGroup(disable);"
            });
            
            chunks.Add(new FunctionChunk
            {
                State = TChunkState.Static,
                Comments = "关闭代码块",
                Name = "VDisabledGroupEnd",
                Params = new FunctionParam[] { },
                Attributes = new List<string> { "ExcludeFromDocs" },
                Content = "EditorGUI.EndDisabledGroup();"
            });
            return chunks;
        }
    }
}