#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO
{
    public partial class GERLayoutSingleton
    {
        [FuncParam(Group = "Change Check", IsArray = true)]
        private static List<FunctionChunk> VCheck()
        {
            var chunks = new List<FunctionChunk>();
            var action = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "开启代码块来检查GUI更改",
                    Name = "VChangeCheck",
                    Params = new FunctionParam[] { action, },
                    Attributes = new List<string> { "ExcludeFromDocs" },
                };
                chunk.ContentBuilder.AppendLine("if (action == null) return;");
                chunk.ContentBuilder.AppendLine("EditorGUI.BeginChangeCheck();");
                chunk.ContentBuilder.AppendLine("action.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUI.EndProperty();");
                chunks.Add(chunk);
            }

            chunks.Add(new FunctionChunk
            {
                State = TChunkState.Static,
                Comments = "启动一个新的代码块来检查GUI更改",
                Name = "VChangeCheckBegin",
                Params = new FunctionParam[] { },
                Attributes = new List<string> { "ExcludeFromDocs" },
                Content = "EditorGUI.BeginChangeCheck();"
            });
            
            chunks.Add(new FunctionChunk
            {
                State = TChunkState.Static,
                Comments = "关闭代码块",
                Name = "VChangeCheckEnd",
                Params = new FunctionParam[] { },
                Attributes = new List<string> { "ExcludeFromDocs" },
                Content = "EditorGUI.EndChangeCheck();"
            });
            return chunks;
        }
    }
}