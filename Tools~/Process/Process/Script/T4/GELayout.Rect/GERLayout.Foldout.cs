#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 

using System.Collections.Generic;

namespace AIO
{
    public partial class GERLayoutSingleton 
    {
        [FuncParam(Group = "Foldout Header Group", IsArray = true)]
        private static List<FunctionChunk> FoldoutHeaderGroup()
        {
            var chunks = new List<FunctionChunk>();
            var rect = new FunctionParam("Rect", "rect", "rect") { Comments = "矩形" };
            var action = new FunctionParam("Action", "action", "") { Comments = "回调函数" };
            var foldout = new FunctionParam("bool", "foldout", "foldout") { Comments = "显示的折叠状态" };
            var style = new FunctionParam("GUIStyle", "style", "style") { Comments = "显示风格" };
            var menuAction = new FunctionParam("Action<Rect>", "menuAction", "menuAction") { Comments = "操作菜单" };
            var menuIcon = new FunctionParam("GUIStyle", "menuIcon", "menuIcon") { Comments = "菜单ICON显示风格" };
            var label = new FunctionParam("GTContent", "label", "label") { Comments = "标签" };

            foreach (var param in new FunctionParam[][]
                     {
                         new FunctionParam[] { rect, label, foldout, action, },
                         new FunctionParam[] { rect, label, foldout, action, style, },
                         new FunctionParam[] { rect, label, foldout, action, style, menuAction, },
                         new FunctionParam[] { rect, label, foldout, action, style, menuAction, menuIcon },
                     })
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "绘制 折页排版",
                    Name = "VFoldoutHeaderGroupRect",
                    Params = param,
                    ReturnType = "bool",
                    ReturnComments = "true:呈现子对象,false:隐藏",
                    Attributes = new List<string> { "ExcludeFromDocs" },
                    MacroDefinition = "UNITY_2020_1_OR_NEWER"
                };
                chunk.ContentBuilder.AppendLine(
                    $"foldout = EditorGUI.BeginFoldoutHeaderGroup({chunk.GetParamValues().Replace("label, foldout", "foldout, label")});");
                chunk.ContentBuilder.AppendLine("if (foldout) action?.Invoke();");
                chunk.ContentBuilder.AppendLine("EditorGUI.EndFoldoutHeaderGroup();");
                chunk.ContentBuilder.AppendLine("return foldout;");
                chunks.Add(chunk);
            }

            foreach (var param in new FunctionParam[][]
                     {
                         new FunctionParam[] { rect, label, foldout, },
                         new FunctionParam[] { rect, label, foldout, style, },
                         new FunctionParam[] { rect, label, foldout, style, menuAction, },
                         new FunctionParam[] { rect, label, foldout, style, menuAction, menuIcon },
                     })
            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "开始绘制 折页排版",
                    Name = "VFoldoutHeaderGroupRectBegin",
                    Params = param,
                    ReturnType = "bool",
                    ReturnComments = "true:呈现子对象,false:隐藏",
                    Attributes = new List<string> { "ExcludeFromDocs" },
                    MacroDefinition = "UNITY_2020_1_OR_NEWER",
                };
                chunk.Content =
                    $"return EditorGUI.BeginFoldoutHeaderGroup({chunk.GetParamValues().Replace("label, foldout", "foldout, label")});";
                chunks.Add(chunk);
            }

            {
                var chunk = new FunctionChunk
                {
                    State = TChunkState.Static,
                    Comments = "结束绘制 折页排版",
                    Name = "VFoldoutHeaderGroupRectEnd",
                    Params = new FunctionParam[] { },
                    Attributes = new List<string> { "ExcludeFromDocs" },
                    MacroDefinition = "UNITY_2020_1_OR_NEWER",
                    Content = "EditorGUI.EndFoldoutHeaderGroup();"
                };
                chunks.Add(chunk);
            }
            return chunks;
        }
    }
}